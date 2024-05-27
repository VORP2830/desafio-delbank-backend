using LocadoraDVD.Domain.Entities;

namespace LocadoraDVD.Domain.Interfaces.Mongo;
public interface IDirectorMongoRepository : IGenericMongoRepository<Director>
{
    Task<IEnumerable<Director>> GetAll();
    Task<Director> GetById(int id);
}
