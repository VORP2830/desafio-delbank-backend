using LocadoraDVD.Application.Dtos;

namespace LocadoraDVD.Application.Interfaces;
public interface IDirectorService
{
    Task<IEnumerable<DirectorDto>> GetAll();
    Task<DirectorDto> GetById(int id);
    Task<DirectorDto> Create(DirectorDto model);
    Task<DirectorDto> Update(DirectorDto model);
    Task<string> Delete(int id);
}
