using System.Text;
using Br.Com.Nieto_Oliveira.Application.Commands;
using Br.Com.Nieto_Oliveira.Application.Interfaces;
using Br.Com.Nieto_Oliveira.Application.Queries;
using Br.Com.Nieto_Oliveira.Domain.Interfaces;
using Br.Com.Nieto_Oliveira.Infrastructure.Repositories;
using Br.Com.Nieto_Oliveira.Infrastructure.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  --- Infraestrutura ---
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

//  --- Segurança ---
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtOptions = new JwtOptions
{
    Key = builder.Configuration["Jwt:Key"]!,
    Issuer = jwtSection["Issuer"]!,
    Audience = jwtSection["Audience"]!,
    AccessTokenMinutes = int.Parse(jwtSection["AccessTokenMinutes"] ?? "15")
};

//  --- Aplicação ---
builder.Services.AddScoped<IAuthCommandHandler, AuthCommandHandler>();
builder.Services.AddScoped<IClienteCommandHandler, ClienteCommandHandler>();
builder.Services.AddScoped<IClienteQueryHandler, ClienteQueryHandler>();

//  --- Autenticação JWT ---
var key = Encoding.UTF8.GetBytes(jwtOptions.Key);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

//  --- Autorização por policies ---
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    options.AddPolicy("UsuarioAutenticado", p => p.RequireAuthenticatedUser());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
