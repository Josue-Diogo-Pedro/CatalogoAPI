using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return Get().Include(p => p.Produtos);
    }
}
