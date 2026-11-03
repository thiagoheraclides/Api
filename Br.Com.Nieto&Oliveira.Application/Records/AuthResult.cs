namespace Br.Com.Nieto_Oliveira.Application.Records;

public record AuthResult(string AccessToken, string RefreshToken, DateTime ExpiraEm);