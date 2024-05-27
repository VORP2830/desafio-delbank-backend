using LocadoraDVD.Domain.Interfaces;
using LocadoraDVD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDVD.Infra.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly MySQLContext _context;
    public GenericRepository(MySQLContext context)
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