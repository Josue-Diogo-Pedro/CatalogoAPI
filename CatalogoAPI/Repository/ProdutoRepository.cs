using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;

namespace CatalogoAPI.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        return Get()
            .OrderBy(nome => nome.Nome)
            .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            .Take(produtosParameters.PageSize)
            .ToList();
    }

    public IEnumerable<Produto> GetProdutosPorPreco() => Get().OrderBy(p => p.Preco).ToList();
}
