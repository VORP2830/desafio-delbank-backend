using LocadoraDVD.Domain.Interfaces.Mongo;
using LocadoraDVD.Infra.Data.Context;

namespace LocadoraDVD.Infra.Data.Repositories.MongoRepositories;
public class GenericMongoRepository<T> : IGenericMongoRepository<T> where T : class
{
    private readonly MongoContext _context;
    public GenericMongoRepository(MongoContext context)
    {
        _context = context;
    }
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    
}
