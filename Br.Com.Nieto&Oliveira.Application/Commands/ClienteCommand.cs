using System.ComponentModel.DataAnnotations;

namespace Br.Com.Nieto_Oliveira.Application.Commands;

public record CriarClienteCommand
{
    [Required, MinLength(3)]
    public string Nome { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; }  = null!;
}

public record AtualizarClienteCommand
{
    public int Id { get; set; }
    
    [Required, MinLength(3)]
    public string Nome { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; }  = null!;
}

public record RemoverClienteCommand
{
    public int Id { get; set; }

    public RemoverClienteCommand(int id)
    {
        Id = id;
    }
}