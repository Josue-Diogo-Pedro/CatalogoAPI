using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

	public ProdutosController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public ActionResult<IEnumerable<Produto>> Get()
	{
		var produtos = _context.Produtos.ToList();
		if(produtos is null)
		{
			return NotFound("Produtos não encontrados...");
		}

		return produtos;
	}

	[HttpGet("{id:int}", Name = "ObterProduto")]
	public ActionResult<Produto> GetById(int id)
	{
		var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
		if (produto is null)
			return NotFound("Produto não encontrado");

		return produto;
	}

	[HttpPost]
	public ActionResult Post(Produto produto)
	{
		if (produto is null)
			return BadRequest();

		_context.Produtos.Add(produto);
		_context.SaveChanges();

		return new CreatedAtRouteResult("ObterProduto",
			new { id = produto.ProdutoId, produto });
	}

	[HttpPut]
	public ActionResult Put(int id, Produto produto)
	{
		if (id != produto.ProdutoId)
			return BadRequest();

		_context.Entry(produto).State = EntityState.Modified;
		_context.SaveChanges();

		return Ok(produto);
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete(int id)
	{
		var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
		if (produto is null)
			return NotFound("Produto não encontrado...");

		_context.Produtos.Remove(produto);
		_context.SaveChanges();

		return Ok(produto);
	}
}
