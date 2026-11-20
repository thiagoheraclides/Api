using Br.Com.Nieto_Oliveira.Domain.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Br.Com.Nieto_Oliveira.Infrastructure.Repositories;

public class RefreshTokenRepository(IConfiguration config) : IRefreshTokenRepository
{
    private readonly IConfiguration _config = config;

    public async Task<(long UsuarioId, bool Revogado, DateTime ExpiraEm)?> ObterAsync(string token, CancellationToken ct = default)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));

        if (conn.State == System.Data.ConnectionState.Closed)
            await conn.OpenAsync(ct);

        const string sql = 
        """
            SELECT  UsuarioId, Revogado, ExpiraEm
            FROM    RefreshTokens
            WHERE   Token = @Token
        """;

        var refresh = await conn.QuerySingleOrDefaultAsync<RefreshTokenRow>(
            new CommandDefinition(sql, new { Token = token }, cancellationToken: ct)
        );

        return refresh == null ? null : (refresh.UsuarioId, refresh.Revogado, refresh.ExpiraEm);
    }

    public async Task RevogarAsync(string token, CancellationToken ct = default)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));

        if (conn.State == System.Data.ConnectionState.Closed)
            await conn.OpenAsync(ct);

        const string sql = 
        """
            UPDATE  RefreshTokens
            SET     Revogado = 1    
            WHERE   Token = @Token
        """;

        await conn.ExecuteAsync(new CommandDefinition(sql, new { Token = token }, cancellationToken: ct));
    }

    public async Task RevogarTodosPorUsuarioAsync(long usuarioId, CancellationToken ct = default)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));

        if (conn.State == System.Data.ConnectionState.Closed)
            await conn.OpenAsync(ct);

        const string sql = 
        """
            UPDATE  RefreshTokens
            SET     Revogado = 1    
            WHERE   UsuarioId = @UsuarioId AND Revogado = 0
        """;

        await conn.ExecuteAsync(new CommandDefinition(sql, new {UsuarioId = usuarioId}, cancellationToken: ct));
    }

    public async Task SalvarAsync(long usuarioId, string token, DateTime expiraEm, CancellationToken ct = default)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));

        if (conn.State == System.Data.ConnectionState.Closed)
            await conn.OpenAsync(ct);

        const string sql = 
        """
            INSERT INTO RefreshTokens 
                        (UsuarioId
                        ,Token
                        ,ExpiraEm)
            VALUES
                        (@UsuarioId
                        ,@Token
                        ,@ExpiraEm)
        """;

        await conn.ExecuteAsync(new CommandDefinition(sql, new {UsuarioId = usuarioId, Token = token, ExpiraEm = expiraEm }, cancellationToken: ct));
    }

    private class RefreshTokenRow
    {
        public int UsuarioId { get; set; }
        public bool Revogado { get; set; }

        public DateTime ExpiraEm { get; set; }
    }
}