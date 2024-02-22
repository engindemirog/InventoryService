using Application.Features.Cars.Commands.Create;
using Application.Features.Cars.Commands.Delete;
using Application.Features.Cars.Commands.Update;
using Application.Features.Cars.Queries.GetById;
using Application.Features.Cars.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Cars.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Car, CreateCarCommand>().ReverseMap();
        CreateMap<Car, CreatedCarResponse>().ReverseMap();
        CreateMap<Car, UpdateCarCommand>().ReverseMap();
        CreateMap<Car, UpdatedCarResponse>().ReverseMap();
        CreateMap<Car, DeleteCarCommand>().ReverseMap();
        CreateMap<Car, DeletedCarResponse>().ReverseMap();
        CreateMap<Car, GetByIdCarResponse>().ReverseMap();
        CreateMap<Car, GetListCarListItemDto>().ReverseMap();
        CreateMap<IPaginate<Car>, GetListResponse<GetListCarListItemDto>>().ReverseMap();
    }
}