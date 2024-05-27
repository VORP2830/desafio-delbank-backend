namespace LocadoraDVD.Domain.Interfaces;

public interface IUnitOfWork
{
    IDvdRepository DvdRepository { get; }
    IDirectorRepository DirectorRepository { get; }
    Task<bool> SaveChangesAsync();
}