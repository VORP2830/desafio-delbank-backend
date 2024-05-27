namespace LocadoraDVD.Domain.Interfaces;

public interface ICache
{
    Task Store<T>(string id, T value, params string[] @params);
    Task<T> Get<T>(string id);
    Task<bool> Delete<T>(string id);
}