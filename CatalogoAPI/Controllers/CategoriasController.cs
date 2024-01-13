using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

	public CategoriasController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet("produtos")]
	public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
	{
		return _context.Categorias.Include(p => p.Produtos).ToList();
	}

	[HttpGet]
	public ActionResult<IEnumerable<Categoria>> Get()
	{
		return _context.Categorias.ToList();
	}


}
