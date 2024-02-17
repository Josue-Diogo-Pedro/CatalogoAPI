using AutoMapper;
using CatalogoAPI.Models;

namespace CatalogoAPI.DTOs.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Produto, ProdutoDTO>().ReverseMap();
		CreateMap<Categoria, CategoriaDTO>().ReverseMap();
	}
}
