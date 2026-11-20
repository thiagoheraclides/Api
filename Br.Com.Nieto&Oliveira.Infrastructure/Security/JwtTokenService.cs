using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Br.Com.Nieto_Oliveira.Application.Interfaces;
using Br.Com.Nieto_Oliveira.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Br.Com.Nieto_Oliveira.Infrastructure.Security;

public class JwtTokenService(JwtOptions options) : ITokenService
{
    private readonly JwtOptions _options = options;

    public TokenResult GerarAccessToken(Usuario usuario)
    {
        var expiraEm = DateTime.Now.AddMinutes(_options.AccessTokenMinutes);
        var claims = new List<Claim>
        {
          new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
          new(JwtRegisteredClaimNames.Email, usuario.Email),
          new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new(ClaimTypes.Name, usuario.Nome),
          new(ClaimTypes.Role, usuario.Role)  
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expiraEm,
            signingCredentials: creds
        );
        return new TokenResult(new JwtSecurityTokenHandler().WriteToken(token), expiraEm);
    }

    public string GerarRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}

public class JwtOptions
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; }  = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenMinutes { get; set; } = 15;
}