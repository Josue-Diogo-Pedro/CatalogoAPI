using AutoMapper;
using CatalogoAPI.DTOs;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repository;
using CatalogoAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//[EnableCors("PermitirApiRequest")]
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
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
    {
        try
        {
            _logger.LogInformation("============== ======================== GET CategoriasProduto =============");
            var categorias = await _uow.CategoriaRepository.GetCategoriasProdutos();

            return _mapper.Map<List<CategoriaDTO>>(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
    {
        try
        {
            _logger.LogInformation("============== ======================== GET Categorias =============");

            var categorias = await _uow.CategoriaRepository.GetCategorias(categoriasParameters);
            if (categorias is null)
                return NotFound("Categorias não encontradas");

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }


    /// <summary>
    /// Bring one Categoria by Id
    /// </summary>
    /// <param name="id">Code of Categoria</param>
    /// <returns>Categoria Object</returns>
    //[EnableCors("PermitirApiRequest")]
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> GetById(int id)
    {
        try
        {
            var categoria = await _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
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

    /// <summary>
    /// Include a new Categoria
    /// </summary>
    /// <remarks>
    /// Request example:
    ///     POST api/categorias
    ///     {
    ///         "categoriaId": 1,
    ///         "nome": "Categoria1",
    ///         "imagemUrl": "http://teste.net/1.jpg"
    ///     }
    /// <param name="categoriaDTO">Categoria Object</param>
    /// <returns>The object Categoria included</returns>
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CategoriaDTO categoriaDTO)
    {
        try
        {
            if (categoriaDTO is null)
                return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            _uow.CategoriaRepository.Add(categoria);
            await _uow.Commit();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaDTO.CategoriaId, categoriaDTO });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }

    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody]CategoriaDTO categoriaDTO)
    {
        try
        {
            if (id != categoriaDTO.CategoriaId)
                return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            _uow.CategoriaRepository.Update(categoria);
            await _uow.Commit();

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
            var categoriaDTO = _mapper.Map<CategoriaDTO>(_uow.CategoriaRepository.GetById(p => p.CategoriaId == id));
            if (categoriaDTO is null)
                return NotFound("Produto não encontrado...");

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            _uow.CategoriaRepository.Delete(categoria);
            await _uow.Commit();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
        }
    }
}
