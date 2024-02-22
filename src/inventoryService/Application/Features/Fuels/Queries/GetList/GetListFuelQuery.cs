using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Fuels.Queries.GetList;

public class GetListFuelQuery : IRequest<GetListResponse<GetListFuelListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFuelQueryHandler : IRequestHandler<GetListFuelQuery, GetListResponse<GetListFuelListItemDto>>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public GetListFuelQueryHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFuelListItemDto>> Handle(GetListFuelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Fuel> fuels = await _fuelRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFuelListItemDto> response = _mapper.Map<GetListResponse<GetListFuelListItemDto>>(fuels);
            return response;
        }
    }
}