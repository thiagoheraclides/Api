using Br.Com.Nieto_Oliveira.Application.Interfaces;
using Br.Com.Nieto_Oliveira.Domain.Entities;
using Br.Com.Nieto_Oliveira.Domain.Interfaces;

namespace Br.Com.Nieto_Oliveira.Application.Commands;

public class ClienteCommandHandler(IClienteRepository repository) : IClienteCommandHandler
{
    private readonly IClienteRepository _repository = repository;

    public async Task<int> Handle(CriarClienteCommand command, CancellationToken ct)
    {
        var cliente = new Cliente(command.Nome, command.Email);
        return _repository.AdicionarAsync(cliente, ct);
    }

    public async Task Handle(AtualizarClienteCommand command, CancellationToken ct)
    {
        var cliente = _repository.ObterPorIdAsync(command.Id, ct)
            ?? throw new InvalidOperationException($"Cliente {command.Id} não encontrado.");

        cliente.Alterar(command.Nome, command.Email);

        _repository.AtualizarAsync(cliente, ct);
    }

    public async Task Handle(RemoverClienteCommand command, CancellationToken ct)
    {
        var cliente = _repository.ObterPorIdAsync(command.Id, ct)
            ?? throw new InvalidOperationException($"Cliente {command.Id} não encontrado.");        

         _repository.RemoverAsync(cliente.Id, ct);
    }
}