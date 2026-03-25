using Estoque.Application.DTOs.Movimentacao;
using Estoque.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MovimentacoesEstoqueController : ControllerBase
{
    private readonly IMovimentacaoEstoqueService _movimentacaoService;

    public MovimentacoesEstoqueController(IMovimentacaoEstoqueService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MovimentacaoEstoqueResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Post(
        [FromBody] MovimentacaoEstoqueRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _movimentacaoService.RegistrarAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("produto/{produtoId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<MovimentacaoEstoqueResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<MovimentacaoEstoqueResponse>>> GetByProdutoId(
        int produtoId,
        CancellationToken cancellationToken)
    {
        try
        {
            var historico = await _movimentacaoService.ObterHistoricoPorProdutoAsync(produtoId, cancellationToken);
            return Ok(historico);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
