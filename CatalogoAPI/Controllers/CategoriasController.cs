using AutoMapper;
using CatalogoAPI.DTOs;
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
    private readonly IMapper _mapper;

    public CategoriasController(IUnitOfWork uow, IConfiguration configuration, ILogger<CategoriasController> logger, IMapper mapper)
    {
        _uow = uow;
        _Configutation = configuration;
        _logger = logger;
        _mapper = mapper;
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
    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
    {
        try
        {
            _logger.LogInformation("============== ======================== GET CategoriasProduto =============");
            var categorias = _uow.CategoriaRepository.GetCategoriasProdutos().ToList();

            return _mapper.Map<List<CategoriaDTO>>(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        try
        {
            _logger.LogInformation("============== ======================== GET Categorias =============");

            var categorias = _uow.CategoriaRepository.Get().ToList();
            if (categorias is null)
                return NotFound("Categorias não encontradas");

            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> GetById(int id)
    {
        try
        {
            var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
            if (categoria is null)
                return NotFound("Produto não encontrado");

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody]CategoriaDTO categoriaDTO)
    {
        try
        {
            if (categoriaDTO is null)
                return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            _uow.CategoriaRepository.Add(categoria);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaDTO.CategoriaId, categoriaDTO });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }

    }

    [HttpPut]
    public ActionResult Put(int id, [FromBody]CategoriaDTO categoriaDTO)
    {
        try
        {
            if (id != categoriaDTO.CategoriaId)
                return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
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
            var categoriaDTO = _mapper.Map<CategoriaDTO>(_uow.CategoriaRepository.GetById(p => p.CategoriaId == id));
            if (categoriaDTO is null)
                return NotFound("Produto não encontrado...");

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
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
