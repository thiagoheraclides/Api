namespace Br.Com.Nieto_Oliveira.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task SalvarAsync(long usuarioId, string token, DateTime expiraEm, CancellationToken ct = default);
    Task<(long UsuarioId, bool Revogado, DateTime ExpiraEm)?> ObterAsync(string token, CancellationToken ct = default);
    Task RevogarAsync(string token, CancellationToken ct = default);
    Task RevogarTodosPorUsuarioAsync(long usuarioId, CancellationToken ct = default);
}