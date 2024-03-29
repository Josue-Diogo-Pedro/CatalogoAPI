﻿using AutoMapper;
using CatalogoAPI.DTOs;
using CatalogoAPI.Filters;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogoAPI.Controllers;

[ApiConventionType(typeof(DefaultApiConventions))]
[Produces("application/json")]
[Route("api/[controller]")]
//[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProdutosController : ControllerBase
{
	private readonly IUnitOfWork _uow;
	private readonly IMapper _mapper;

	public ProdutosController(IUnitOfWork uow, IMapper mapper)
	{
		_uow = uow;
		_mapper = mapper;
	}

	[HttpGet("menor-preco")]
	public async Task<IEnumerable<ProdutoDTO>> GetProdutosPreco() => _mapper.Map<IEnumerable<ProdutoDTO>>(await _uow.ProdutoRepository.GetProdutosPorPreco());

	/// <summary>
	///	Show the list of products
	/// </summary>
	/// <param name="produtosParameters">We can pass the number of page and the number of elements that we want to see</param>
	/// <returns>List of products</returns>
	[HttpGet]
	[ServiceFilter(typeof(ApiLoggingFilter))]
    [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery]ProdutosParameters produtosParameters)
	{
		try
		{
			var produtos = await _uow.ProdutoRepository.GetProdutos(produtosParameters);
			if (produtos is null)
			{
				return NotFound("Produtos não encontrados...");
			}

			var metadata = new
			{
				produtos.TotalCount,
				produtos.PageSize,
				produtos.CurrentPage,
				produtos.TotalPages,
				produtos.HasNext,
				produtos.HasPrevious
			};

			Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

			var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
			return produtosDTO;
		}
		catch (Exception)
		{

			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}

	[HttpGet("{valor:alpha}")]
	public ActionResult<ProdutoDTO> Get2()
	{
		var produto = _uow.ProdutoRepository.Get().FirstOrDefault();
		var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
		return produtoDTO;
	}

	/// <summary>
	/// Bring a product by Id
	/// </summary>
	/// <param name="id">Product code</param>
	/// <returns>Product object</returns>
	//public async Task<ActionResult<Produto>> GetById8(int id, [BindRequired]string nome)
	[HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProdutoDTO>> GetById(int id)
	{
		try
		{
			var produto = await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
			if (produto is null)
				return NotFound("Produto não encontrado");

			var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
			return produtoDTO;
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}

	[HttpPost]
    [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDTO)
	{
		try
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var produto = _mapper.Map<Produto>(produtoDTO);
			_uow.ProdutoRepository.Add(produto);
			await _uow.Commit();

			return new CreatedAtRouteResult("ObterProduto",
				new { id = produtoDTO.ProdutoId, produtoDTO });
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}
	}

	[HttpPut]
    [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
	public async Task<ActionResult> Put(int id, [FromBody]ProdutoDTO produtoDTO)
	{
		try
		{
			if (id != produtoDTO.ProdutoId)
				return BadRequest();

			var produto = _mapper.Map<Produto>(produtoDTO);
			_uow.ProdutoRepository.Update(produto);
			await _uow.Commit();

			return Ok(produtoDTO);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}

	[HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
	{
		try
		{
			var produtoDTO = _mapper.Map<ProdutoDTO>(await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id));
			if (produtoDTO is null)
				return NotFound("Produto não encontrado...");

			var produto = _mapper.Map<Produto>(produtoDTO);
			_uow.ProdutoRepository.Delete(produto);
			await _uow.Commit();

			return Ok(produtoDTO);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao devolver a solicitação");
		}

	}
}
