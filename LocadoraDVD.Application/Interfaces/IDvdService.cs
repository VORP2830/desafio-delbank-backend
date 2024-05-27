using LocadoraDVD.Application.Dtos;

namespace LocadoraDVD.Application.Interfaces;
public interface IDvdService
{
    Task<IEnumerable<DvdDto>> GetAll();
    Task<DvdDto> GetById(int id);
    Task<DvdDto> Create(DvdDto model);
    Task<DvdDto> Update(DvdDto model);
    Task<string> Delete(int id);
}
