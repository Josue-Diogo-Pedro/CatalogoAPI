using CatalogoAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogoAPI.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected AppDbContext _context;

    public Repository(AppDbContext context) => _context = context; 

    public IQueryable<T> Get() => _context.Set<T>().AsNoTracking();

    public T GetById(Expression<Func<T, bool>> predicate) => _context.Set<T>().FirstOrDefault(predicate);

    public void Add(T entity) => _context.Set<T>().Add(entity);

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity) => _context.Set<T>().Remove(entity);
}
