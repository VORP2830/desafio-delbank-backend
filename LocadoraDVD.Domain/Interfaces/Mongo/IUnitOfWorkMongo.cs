namespace LocadoraDVD.Domain.Interfaces.Mongo;
public interface IUnitOfWorkMongo
{
    IDvdMongoRepository DvdMongoRepository { get; }
    IDirectorMongoRepository DirectorMongoRepository { get; }
    Task<bool> SaveChangesAsync();
}
