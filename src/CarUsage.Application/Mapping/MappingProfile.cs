using AutoMapper;
using CarUsage.Application.Features.Vehicle.Create;
using CarUsage.Application.Features.Vehicle.GetAll;
using CarUsage.Domain.Entities;

namespace CarUsage.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCommandRequest, Vehicle>().ReverseMap();
        CreateMap<Vehicle, GetAllQueryResponse>().ReverseMap();
    }
}