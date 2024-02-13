using CatalogoAPI.Context;
using CatalogoAPI.Models;

namespace CatalogoAPI.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Produto> GetProdutosPorPreco() => Get().OrderBy(p => p.Preco).ToList();
}
