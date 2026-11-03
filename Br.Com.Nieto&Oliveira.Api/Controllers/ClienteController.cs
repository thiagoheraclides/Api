using Br.Com.Nieto_Oliveira.Application.Commands;
using Br.Com.Nieto_Oliveira.Application.Interfaces;
using Br.Com.Nieto_Oliveira.Application.Queries;
using Br.Com.Nieto_Oliveira.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Br.Com.Nieto_Oliveira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController(
    IClienteCommandHandler commandHandler,
    IClienteQueryHandler queryHandler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> ObterTodosAsync(CancellationToken ct)
    {
        var clientes = await queryHandler.Handle(new ObterTodosClientesQuery(), ct);
        return Ok(clientes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cliente>> ObterPorIdAsync(int id, CancellationToken ct)
    {
        var cliente = await queryHandler.Handle(new ObterClientePorIdQuery(id), ct);
        return cliente == null
               ? NotFound(new { mensagem = $"Cliente {id} não encontrado."})
               : Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> ObterPorIdAsync(CriarClienteCommand command, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await commandHandler.Handle(command, ct);
        return CreatedAtAction(nameof(CriarClienteCommand), new {id});
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, AtualizarClienteCommand command, CancellationToken ct)
    {
        if (id != command.Id)
            return BadRequest(new { mensagem = "Id da rota difere do corpo da requisição." });

        await commandHandler.Handle(command, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id, CancellationToken ct)
    {
        await commandHandler.Handle(new RemoverClienteCommand(id), ct);
        return NoContent();
    }
}