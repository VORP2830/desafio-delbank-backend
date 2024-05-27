using LocadoraDVD.Domain.Entities;

namespace LocadoraDVD.Domain.Interfaces;
public interface IDirectorRepository : IGenericRepository<Director>
{
    Task<Director> GetById(int id);
}
