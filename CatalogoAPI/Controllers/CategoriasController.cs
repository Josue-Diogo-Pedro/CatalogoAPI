using CatalogoAPI.Models;
using CatalogoAPI.Repository;
using CatalogoAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
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
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            _logger.LogInformation("============== ======================== GET Categorias =============");

            var categorias = _uow.CategoriaRepository.Get().ToList();
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
            var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
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

            _uow.CategoriaRepository.Add(categoria);
            _uow.Commit();

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

            _uow.CategoriaRepository.Update(categoria);
            _uow.Commit();

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
            var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
            if (categoria is null)
                return NotFound("Produto não encontrado...");

            _uow.CategoriaRepository.Delete(categoria);
            _uow.Commit();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }
}
