using CatalogoAPI.Filters;
using CatalogoAPI.Models;
using CatalogoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers;

[Route("api/[controller]")]
//[ApiController]
public class ProdutosController : ControllerBase
{
	private readonly IUnitOfWork _uow;

	public ProdutosController(IUnitOfWork uow)
	{
		_uow = uow;
	}

	[HttpGet("menor-preco")]
	public IEnumerable<Produto> GetProdutosPreco() => _uow.ProdutoRepository.GetProdutosPorPreco().ToList();

	[HttpGet]
	[ServiceFilter(typeof(ApiLoggingFilter))]
	public ActionResult<IEnumerable<Produto>> Get()
	{
		try
		{
			var produtos = _uow.ProdutoRepository.Get().ToList();
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
	public ActionResult<Produto> Get2()
	{
		var produto = _uow.ProdutoRepository.Get().FirstOrDefault();
		return produto;
	}

	//public async Task<ActionResult<Produto>> GetById8(int id, [BindRequired]string nome)
	[HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
	public ActionResult<Produto> GetById(int id)
	{
		try
		{
			var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
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
	public ActionResult Post([FromBody] Produto produto)
	{
		try
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_uow.ProdutoRepository.Add(produto);
			_uow.Commit();

			return new CreatedAtRouteResult("ObterProduto",
				new { id = produto.ProdutoId, produto });
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}
	}

	[HttpPut]
	public ActionResult Put(int id, [FromBody]Produto produto)
	{
		try
		{
			if (id != produto.ProdutoId)
				return BadRequest();

			_uow.ProdutoRepository.Update(produto);
			_uow.Commit();

			return Ok(produto);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}

	[HttpDelete("{id:int:min(1)}")]
	public ActionResult Delete(int id)
	{
		try
		{
			var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
			if (produto is null)
				return NotFound("Produto não encontrado...");

			_uow.ProdutoRepository.Delete(produto);
			_uow.Commit();

			return Ok(produto);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}
}
