using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Interfaces;
using LocadoraDVD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDVD.Infra.Data.Repositories;

public class DvdRepository : GenericRepository<Dvd>, IDvdRepository
{
    private readonly MySQLContext _context;
    public DvdRepository(MySQLContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Dvd> GetById(int id)
    {
        return await _context.Dvds
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.DeletedAt == null &&
                                x.Id == id);
    }
}
