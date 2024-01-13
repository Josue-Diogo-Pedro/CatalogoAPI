using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
}
