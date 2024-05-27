using AutoMapper;
using LocadoraDVD.Domain.Entities;

namespace LocadoraDVD.Application.Dtos.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Dvd, DvdDto>().ReverseMap();
        CreateMap<Director, DirectorDto>().ReverseMap();
    }
}