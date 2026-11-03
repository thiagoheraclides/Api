using Br.Com.Nieto_Oliveira.Application.Commands;
using Br.Com.Nieto_Oliveira.Application.Records;

namespace Br.Com.Nieto_Oliveira.Application.Interfaces;

public interface IAuthCommandHandler
{
    Task<AuthResult> Handle (LoginCommand command, CancellationToken ct);
    Task<AuthResult> Handle (RefreshCommand command, CancellationToken ct);
    Task Handle (LogoutCommand command, CancellationToken ct);
    Task<int> Handle (RegistrarUsuarioCommand command, CancellationToken ct);
}