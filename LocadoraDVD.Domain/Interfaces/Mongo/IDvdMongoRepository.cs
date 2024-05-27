using LocadoraDVD.Domain.Entities;

namespace LocadoraDVD.Domain.Interfaces.Mongo;
public interface IDvdMongoRepository : IGenericMongoRepository<Dvd>
{
    Task<IEnumerable<Dvd>> GetAll();
    Task<Dvd> GetById(int id);
}
