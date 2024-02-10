using CatalogoAPI.Context;
using CatalogoAPI.Filters;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers;

[Route("api/[controller]")]
//[ApiController]
public class ProdutosController : ControllerBase
{
	private readonly AppDbContext _context;

	public ProdutosController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	[ServiceFilter(typeof(ApiLoggingFilter))]
	public async Task<ActionResult<IEnumerable<Produto>>> Get()
	{
		try
		{
			var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
			if (produtos is null)
			{
				return NotFound("Produtos não encontrados...");
			}

			return produtos;
		}
		catch (Exception)
		{

			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}

	[HttpGet("{valor:alpha}")]
	public async Task<ActionResult<Produto>> Get2()
	{
		var produto = await _context.Produtos.FirstOrDefaultAsync();
		return produto;
	}

	//public async Task<ActionResult<Produto>> GetById8(int id, [BindRequired]string nome)
	[HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
	public async Task<ActionResult<Produto>> GetById(int id)
	{
		try
		{
			var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
			if (produto is null)
				return NotFound("Produto não encontrado");

			return produto;
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}

	[HttpPost]
	public async Task<ActionResult> Post([FromBody] Produto produto)
	{
		try
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _context.Produtos.AddAsync(produto);
			await _context.SaveChangesAsync();

			return new CreatedAtRouteResult("ObterProduto",
				new { id = produto.ProdutoId, produto });
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}
	}

	[HttpPut]
	public async Task<ActionResult> Put(int id, Produto produto)
	{
		try
		{
			if (id != produto.ProdutoId)
				return BadRequest();

			_context.Entry(produto).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(produto);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}

	[HttpDelete("{id:int:min(1)}")]
	public async Task<ActionResult> Delete(int id)
	{
		try
		{
			var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
			if (produto is null)
				return NotFound("Produto não encontrado...");

			_context.Produtos.Remove(produto);
			await _context.SaveChangesAsync();

			return Ok(produto);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}
}
