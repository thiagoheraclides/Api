using Br.Com.Nieto_Oliveira.Domain.Entities;

namespace Br.Com.Nieto_Oliveira.Application.Interfaces;

public record TokenResult(string AccessToken, DateTime ExpiraEm);

public interface ITokenService
{
    TokenResult GerarAccessToken(Usuario usuario);
    string GerarRefreshToken();
}