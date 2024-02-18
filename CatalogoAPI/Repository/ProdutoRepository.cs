using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context) { }

    public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
    {
        //return Get()
        //    .OrderBy(nome => nome.Nome)
        //    .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //    .Take(produtosParameters.PageSize)
        //    .ToList();

        return await PagedList<Produto>.ToPagedList(Get().OrderBy(order => order.ProdutoId), 
            produtosParameters.PageNumber, produtosParameters.PageSize);
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorPreco() => await Get().OrderBy(p => p.Preco).ToListAsync();
}
