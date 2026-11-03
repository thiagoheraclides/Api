using Br.Com.Nieto_Oliveira.Domain.Entities;

namespace Br.Com.Nieto_Oliveira.Domain.Interfaces;

public interface IClienteRepository
{
    Cliente? ObterPorIdAsync (int id, CancellationToken cancellationToken = default);
    IEnumerable<Cliente> ObterTodosAsync (CancellationToken cancellationToken = default);
    int AdicionarAsync (Cliente cliente, CancellationToken cancellationToken = default);
    void AtualizarAsync (Cliente cliente, CancellationToken cancellationToken = default);
    void RemoverAsync (int id, CancellationToken cancellationToken = default);
}