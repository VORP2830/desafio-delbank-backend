using LocadoraDVD.Domain.Interfaces;
using LocadoraDVD.Infra.Data.Context;

namespace LocadoraDVD.Infra.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MySQLContext _context;
    public UnitOfWork(MySQLContext context)
    {
        _context = context;
    }
    public IDvdRepository DvdRepository => new DvdRepository(_context);
    public IDirectorRepository DirectorRepository => new DirectorRepository(_context);
    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
