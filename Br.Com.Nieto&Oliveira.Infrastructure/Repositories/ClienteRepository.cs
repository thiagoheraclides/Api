using Br.Com.Nieto_Oliveira.Domain.Entities;
using Br.Com.Nieto_Oliveira.Domain.Interfaces;

namespace Br.Com.Nieto_Oliveira.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private ICollection<Cliente> clientes = [];

    public int AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        int id = clientes.Count;
        cliente.Id = id + 1;
        clientes.Add(cliente);
        return cliente.Id;
    }

    public void AtualizarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        var antigo = clientes.FirstOrDefault(x => x.Id == cliente.Id)
            ?? throw new InvalidOperationException($"Cliente {cliente.Id} não encontrado.");
        clientes.Remove(antigo);
        clientes.Add(cliente);
    }

    public Cliente? ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return clientes.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Cliente> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return clientes;
    }

    public void RemoverAsync(int id, CancellationToken cancellationToken = default)
    {
        var cliente = clientes.FirstOrDefault(x => x.Id == id)
            ?? throw new InvalidOperationException($"Cliente {id} não encontrado.");
        clientes.Remove(cliente);
    }
}