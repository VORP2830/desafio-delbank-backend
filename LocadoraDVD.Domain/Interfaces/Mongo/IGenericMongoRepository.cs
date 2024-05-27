namespace LocadoraDVD.Domain.Interfaces.Mongo;
public interface IGenericMongoRepository<T>
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
