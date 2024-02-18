using CatalogoAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ProdutoRepository _produtoRepository;
    private CategoriaRepository _categoriaRepository;
    private AppDbContext _context;

    public UnitOfWork(AppDbContext context) => _context = context;

    public IProdutoRepository ProdutoRepository 
    {
        get
        {
            return _produtoRepository ?? new ProdutoRepository(_context);
        }
    }

    public ICategoriaRepository CategoriaRepository 
    {
        get
        {
            return _categoriaRepository ?? new CategoriaRepository(_context);
        }
    }

    public async Task Commit() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
