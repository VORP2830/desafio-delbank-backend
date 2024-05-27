using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Interfaces;
using LocadoraDVD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDVD.Infra.Data.Repositories;
public class DirectorRepository : GenericRepository<Director>, IDirectorRepository
{
    private readonly MySQLContext _context;
    public DirectorRepository(MySQLContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Director> GetById(int id)
    {
        return await _context.Directors
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.DeletedAt == null &&
                                x.Id == id);
    }
}
