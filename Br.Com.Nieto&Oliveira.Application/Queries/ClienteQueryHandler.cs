using Br.Com.Nieto_Oliveira.Application.Interfaces;
using Br.Com.Nieto_Oliveira.Domain.Entities;
using Br.Com.Nieto_Oliveira.Domain.Interfaces;

namespace Br.Com.Nieto_Oliveira.Application.Queries;

public class ClienteQueryHandler(IClienteRepository repository) : IClienteQueryHandler
{
    private readonly IClienteRepository _repository = repository;

    public async Task<Cliente?> Handle(ObterClientePorIdQuery query, CancellationToken ct)
        => _repository.ObterPorIdAsync(query.Id, ct);

    public async Task<IEnumerable<Cliente>> Handle(ObterTodosClientesQuery query, CancellationToken ct)
        => _repository.ObterTodosAsync();
}