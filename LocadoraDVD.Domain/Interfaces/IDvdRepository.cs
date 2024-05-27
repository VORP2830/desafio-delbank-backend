using LocadoraDVD.Domain.Entities;

namespace LocadoraDVD.Domain.Interfaces;
public interface IDvdRepository : IGenericRepository<Dvd>
{
    Task<Dvd> GetById(int id);
}
