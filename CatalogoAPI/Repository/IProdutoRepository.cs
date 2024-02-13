using CatalogoAPI.Models;

namespace CatalogoAPI.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorPreco();
}
