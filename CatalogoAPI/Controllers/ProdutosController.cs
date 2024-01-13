using CatalogoAPI.Context;
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


}
