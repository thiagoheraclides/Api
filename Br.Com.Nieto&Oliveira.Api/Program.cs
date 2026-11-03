using Br.Com.Nieto_Oliveira.Application.Commands;
using Br.Com.Nieto_Oliveira.Application.Interfaces;
using Br.Com.Nieto_Oliveira.Application.Queries;
using Br.Com.Nieto_Oliveira.Domain.Interfaces;
using Br.Com.Nieto_Oliveira.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteCommandHandler, ClienteCommandHandler>();
builder.Services.AddScoped<IClienteQueryHandler, ClienteQueryHandler>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
