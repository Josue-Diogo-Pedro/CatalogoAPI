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
        try
        {
            return _context.Categorias.AsNoTracking().Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
	}

	[HttpGet]
	public ActionResult<IEnumerable<Categoria>> Get()
	{
        try
        {
            var categorias = _context.Categorias.AsNoTracking().ToList();
            if (categorias is null)
                return NotFound("Categorias não encontradas");

            return categorias;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
	}

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> GetById(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null)
                return NotFound("Produto não encontrado");

            return categoria;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        try
        {
            if (categoria is null)
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId, categoria });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
        
    }

    [HttpPut]
    public ActionResult Put(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
        
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null)
                return NotFound("Produto não encontrado...");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }
}
