using Estoque.Application.DTOs.Produto;
using Estoque.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProdutoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProdutoResponse>>> Get(CancellationToken cancellationToken)
    {
        var produtos = await _produtoService.ObterTodosAsync(cancellationToken);
        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var produto = await _produtoService.ObterPorIdAsync(id, cancellationToken);

        if (produto is null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProdutoResponse>> Post(
        [FromBody] ProdutoRequest request,
        CancellationToken cancellationToken)
    {
        var produto = await _produtoService.CriarAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoResponse>> Put(
        int id,
        [FromBody] ProdutoRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var produto = await _produtoService.AtualizarAsync(id, request, cancellationToken);
            return Ok(produto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var removido = await _produtoService.RemoverAsync(id, cancellationToken);

        if (!removido)
        {
            return NotFound();
        }

        return NoContent();
    }
}
