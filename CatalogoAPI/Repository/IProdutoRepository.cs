﻿using CatalogoAPI.Models;
using CatalogoAPI.Pagination;

namespace CatalogoAPI.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
    Task<IEnumerable<Produto>> GetProdutosPorPreco();
}
