using LocadoraDVD.Domain.Interfaces.Mongo;
using LocadoraDVD.Infra.Data.Context;

namespace LocadoraDVD.Infra.Data.Repositories.MongoRepositories;
public class UnitOfWorkMongo : IUnitOfWorkMongo
{
    private readonly MongoContext _context;
    public UnitOfWorkMongo(MongoContext context)
    {
        _context = context;
    }
    public IDvdMongoRepository DvdMongoRepository => new DvdMongoRepository(_context);

    public IDirectorMongoRepository DirectorMongoRepository => new DirectorMongoRepository(_context);

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}