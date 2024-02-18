using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;

namespace CatalogoAPI.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context) { }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        //return Get()
        //    .OrderBy(nome => nome.Nome)
        //    .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //    .Take(produtosParameters.PageSize)
        //    .ToList();

        return PagedList<Produto>.ToPagedList(Get().OrderBy(order => order.ProdutoId), 
            produtosParameters.PageNumber, produtosParameters.PageSize);
    }

    public IEnumerable<Produto> GetProdutosPorPreco() => Get().OrderBy(p => p.Preco).ToList();
}
