using System.ComponentModel.DataAnnotations;

namespace Br.Com.Nieto_Oliveira.Application.Commands;

public record LoginCommand(
    [Required, EmailAddress] string Email,
    [Required,MinLength(8)] string Senha
);

public record RefreshCommand(
    [Required] string RefreshToken
);

public record LogoutCommand(
    [Required] string RefreshToken
);

public record RegistrarUsuarioCommand(
    [Required,MinLength(3)] string Nome,
    [Required, EmailAddress] string Email,
    [Required,MinLength(8)] string Senha,
    string Role = "USR"
);