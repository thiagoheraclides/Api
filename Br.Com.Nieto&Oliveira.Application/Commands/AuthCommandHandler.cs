using Br.Com.Nieto_Oliveira.Application.Interfaces;
using Br.Com.Nieto_Oliveira.Application.Records;
using Br.Com.Nieto_Oliveira.Domain.Entities;
using Br.Com.Nieto_Oliveira.Domain.Interfaces;

namespace Br.Com.Nieto_Oliveira.Application.Commands;

public class AuthCommandHandler(
    IUsuarioRepository usuarios,
    IRefreshTokenRepository refreshTokens,
    ITokenService tokenService,
    IPasswordHasher hasher
    ) : IAuthCommandHandler
{

    private readonly IUsuarioRepository _usuarios = usuarios;
    private readonly IRefreshTokenRepository _refreshTokens = refreshTokens;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IPasswordHasher _hasher = hasher;

    public async Task<AuthResult> Handle(LoginCommand command, CancellationToken ct)
    {
        var usuario = await _usuarios.ObterPorEmailAsync(command.Email, ct);

        if (usuario is null || !usuario.Ativo || !_hasher.Verificar(command.Senha, usuario.Senha))
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        var access = _tokenService.GerarAccessToken(usuario);
        var refresh = _tokenService.GerarRefreshToken();

        await _refreshTokens.SalvarAsync(usuario.Id, refresh, DateTime.Now.AddDays(7), ct);

        return new AuthResult(access.AccessToken, refresh, access.ExpiraEm);
    }

    public async Task<AuthResult> Handle(RefreshCommand command, CancellationToken ct)
    {
        var registro = await _refreshTokens.ObterAsync(command.RefreshToken, ct);

        if (registro is null || registro.Value.Revogado || registro.Value.ExpiraEm <= DateTime.Now)
            throw new UnauthorizedAccessException("Refresh token inválido ou expirado");

        var usuario = await _usuarios.ObterPorIdAsync(registro.Value.UsuarioId, ct)
            ?? throw new UnauthorizedAccessException("Usuário não encontrado.");

        await _refreshTokens.RevogarAsync(command.RefreshToken, ct);

        var access = _tokenService.GerarAccessToken(usuario);
        var refresh = _tokenService.GerarRefreshToken();

        await _refreshTokens.SalvarAsync(usuario.Id, refresh, DateTime.Now.AddDays(7), ct);

        return new AuthResult(access.AccessToken, refresh, access.ExpiraEm);
    }

    public async Task Handle(LogoutCommand command, CancellationToken ct) 
        => await _refreshTokens.RevogarAsync(command.RefreshToken, ct);

    public async Task<int> Handle(RegistrarUsuarioCommand command, CancellationToken ct)
    {
        var existente = await _usuarios.ObterPorEmailAsync(command.Email, ct);
        if (existente != null)
            throw new InvalidOperationException("E-mail já cadastrado");

        var hash = _hasher.GerarHash(command.Senha);
        var usuario = new Usuario(command.Nome, command.Email, hash, command.Role);

        return await _usuarios.AdicionarAsync(usuario, ct);
    }
}

