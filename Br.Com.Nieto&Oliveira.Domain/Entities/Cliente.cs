namespace Br.Com.Nieto_Oliveira.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public DateTime CriadoEm { get; private set; }

    public Cliente(string nome, string email)
    {
        Nome = nome;
        Email = email;
        CriadoEm = DateTime.Now;
    }

    protected Cliente() {}

    public void Alterar (string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}