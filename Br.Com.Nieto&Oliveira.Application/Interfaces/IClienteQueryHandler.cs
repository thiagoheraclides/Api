using Br.Com.Nieto_Oliveira.Domain.Entities;
using Br.Com.Nieto_Oliveira.Application.Queries;

namespace Br.Com.Nieto_Oliveira.Application.Interfaces;

public interface IClienteQueryHandler
{
    Task<Cliente?> Handle(ObterClientePorIdQuery query, CancellationToken ct);
    Task<IEnumerable<Cliente>> Handle(ObterTodosClientesQuery query, CancellationToken ct);
}