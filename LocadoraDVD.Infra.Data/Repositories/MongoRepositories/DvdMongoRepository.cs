using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Interfaces.Mongo;
using LocadoraDVD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDVD.Infra.Data.Repositories.MongoRepositories;
public class DvdMongoRepository  : GenericMongoRepository<Dvd>, IDvdMongoRepository
{
    private readonly MongoContext _context;
    public DvdMongoRepository(MongoContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Dvd>> GetAll()
    {
        return await _context.Dvds
                                .AsNoTracking()
                                .Where(x => x.DeletedAt == null)
                                .ToListAsync();
    }
    public async Task<Dvd> GetById(int id)
    {
        return await _context.Dvds
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.DeletedAt == null && 
                                x.Id == id);
    }
}
