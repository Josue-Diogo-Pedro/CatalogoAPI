using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Repository;
using CatalogoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _Configutation;
    private readonly ILogger _logger;

	public CategoriasController(IUnitOfWork uow, IConfiguration configuration, ILogger<CategoriasController> logger)
	{
		_uow = uow;
        _Configutation = configuration;
        _logger = logger;
	}

    [HttpGet("autor")]
    public string GetAutor()
    {
        var autor = $"Autor: {_Configutation["autor"]}";
        var conexao = $"Conexão: {_Configutation["ConnectionStrings:DefaultConnection"]}";
        return conexao;
    }

    [HttpGet("saudacao/{nome}")]
    public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuServico, string nome)
    {
        return meuServico.Saudacao(nome);
    }

	[HttpGet("produtos")]
	public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
	{
        try
        {
            _logger.LogInformation("============== ======================== GET CategoriasProduto =============");
            return _uow.CategoriaRepository.GetCategoriasProdutos().ToList();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
	}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get()
	{
        try
        {
            _logger.LogInformation("============== ======================== GET Categorias =============");

            var categorias = await _uow.Categorias.AsNoTracking().ToListAsync();
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
    public async Task<ActionResult<Categoria>> GetById(int id)
    {
        try
        {
            var categoria = await _uow.Categorias.FirstOrDefaultAsync(p => p.CategoriaId == id);
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
    public async Task<ActionResult> Post(Categoria categoria)
    {
        try
        {
            if (categoria is null)
                return BadRequest();

            await _uow.Categorias.AddAsync(categoria);
            await _uow.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId, categoria });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
        
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _uow.Entry(categoria).State = EntityState.Modified;
            await _uow.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
        
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var categoria = await _uow.Categorias.FirstOrDefaultAsync(p => p.CategoriaId == id);
            if (categoria is null)
                return NotFound("Produto não encontrado...");

            _uow.Categorias.Remove(categoria);
            await _uow.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }
}
