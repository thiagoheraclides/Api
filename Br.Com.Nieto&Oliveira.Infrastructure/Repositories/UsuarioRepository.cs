using Br.Com.Nieto_Oliveira.Domain.Entities;
using Br.Com.Nieto_Oliveira.Domain.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Br.Com.Nieto_Oliveira.Infrastructure.Repositories;

public class UsuarioRepository(IConfiguration config) : IUsuarioRepository
{
    private readonly IConfiguration _config = config;

    public async Task<int> AdicionarAsync(Usuario usuario, CancellationToken ct = default)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));

        if (conn.State == System.Data.ConnectionState.Closed)
            await conn.OpenAsync(ct);

        const string sql = 
        """
            INSERT INTO Usuarios (Nome, Email, Senha, Role, Ativo)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Email, @Senha, @Role, @Ativo)
        """;

        return await conn.ExecuteScalarAsync<int>(new CommandDefinition(sql, usuario, cancellationToken: ct));
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken ct = default)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));
        
        if (conn.State == System.Data.ConnectionState.Closed)
            await conn.OpenAsync(ct);

        const string sql = 
        """
            SELECT  Id, Nome, Email, Senha, Role, Ativo
            FROM    Usuarios
            WHERE   Email = @Email
        """;

        return await conn.QuerySingleOrDefaultAsync<Usuario>(new CommandDefinition(sql, new { Email = email }, cancellationToken: ct));
    }

    public async Task<Usuario?> ObterPorIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));
        
        if (conn.State == System.Data.ConnectionState.Closed)
            await conn.OpenAsync(ct);

        const string sql = 
        """
            SELECT  Id, Nome, Email, Senha, Role, Ativo
            FROM    Usuarios
            WHERE   Id = @Id
        """;

        return await conn.QuerySingleOrDefaultAsync<Usuario>(new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
    }
}