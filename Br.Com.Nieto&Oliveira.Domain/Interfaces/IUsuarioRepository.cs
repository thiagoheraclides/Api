using Br.Com.Nieto_Oliveira.Domain.Entities;

namespace Br.Com.Nieto_Oliveira.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken ct = default);
    Task<Usuario?> ObterPorIdAsync(long id, CancellationToken ct = default);
    Task<int> AdicionarAsync(Usuario usuario, CancellationToken ct = default);
}