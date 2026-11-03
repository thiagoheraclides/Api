namespace Br.Com.Nieto_Oliveira.Application.Queries;

public record ObterClientePorIdQuery
{
    public int Id { get; set; }

    public ObterClientePorIdQuery(int id)
    {
        Id = id;
    }
}

public record ObterTodosClientesQuery { }