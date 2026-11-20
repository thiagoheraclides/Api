using Br.Com.Nieto_Oliveira.Application.Interfaces;

namespace Br.Com.Nieto_Oliveira.Infrastructure.Security;

public class BcryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;

    public string GerarHash(string senha)
        => BCrypt.Net.BCrypt.HashPassword(senha, WorkFactor);

    public bool Verificar(string senha, string hash )
        => BCrypt.Net.BCrypt.Verify(senha, hash);
}