using Br.Com.Nieto_Oliveira.Application.Commands;

namespace Br.Com.Nieto_Oliveira.Application.Interfaces;

public interface IClienteCommandHandler
{
    Task<int> Handle(CriarClienteCommand command, CancellationToken ct);
    Task Handle(AtualizarClienteCommand command, CancellationToken ct);
    Task Handle(RemoverClienteCommand command, CancellationToken ct);
}