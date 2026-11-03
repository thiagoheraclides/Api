namespace Br.Com.Nieto_Oliveira.Domain.Entities;

public class Usuario
{
    public long Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;
    public string Role { get; private set; } = "USR";
    public bool Ativo { get; private set; } = true;

    public Usuario(string nome, string email, string senha, string role = "USR")
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Role = role;
    }

    protected Usuario() { }
}