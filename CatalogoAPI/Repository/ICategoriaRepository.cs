using CatalogoAPI.Models;
using CatalogoAPI.Pagination;

namespace CatalogoAPI.Repository;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
    Task<IEnumerable<Categoria>> GetCategoriasProdutos();
}
