using CatalogoAPI.Context;
using System.Linq.Expressions;

namespace CatalogoAPI.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected AppDbContext _context;

    public Repository()
    {

    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Get()
    {
        throw new NotImplementedException();
    }

    public T GetById(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
