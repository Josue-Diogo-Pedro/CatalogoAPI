using CatalogoAPI.Models;
using GraphQL.Types;

namespace CatalogoAPI.GraphQL;

//Estamos definindo qual entidade será mapeada para o nosso Type
public class CategoriaType : ObjectGraphType<Categoria>
{
	public CategoriaType()
	{
		//Campos Type
		Field(x => x.CategoriaId);
		Field(x => x.Nome);
		Field(x => x.ImagemUrl);

		Field<ListGraphType<CategoriaType>>("categorias");
	}
}
