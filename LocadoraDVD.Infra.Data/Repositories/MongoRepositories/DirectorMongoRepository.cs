using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Interfaces.Mongo;
using LocadoraDVD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDVD.Infra.Data.Repositories.MongoRepositories;
public class DirectorMongoRepository : GenericMongoRepository<Director>, IDirectorMongoRepository
{
    private readonly MongoContext _context;
    public DirectorMongoRepository(MongoContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Director>> GetAll()
    {
        return await _context.Directors
                                .AsNoTracking()
                                .Where(x => x.DeletedAt == null)
                                .ToListAsync();
    }
    public async Task<Director> GetById(int id)
    {
        return await _context.Directors
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.DeletedAt == null && 
                                                                x.Id == id);
    }
}
