using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Transmissions.Queries.GetList;

public class GetListTransmissionQuery : IRequest<GetListResponse<GetListTransmissionListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransmissionQueryHandler : IRequestHandler<GetListTransmissionQuery, GetListResponse<GetListTransmissionListItemDto>>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public GetListTransmissionQueryHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTransmissionListItemDto>> Handle(GetListTransmissionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Transmission> transmissions = await _transmissionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTransmissionListItemDto> response = _mapper.Map<GetListResponse<GetListTransmissionListItemDto>>(transmissions);
            return response;
        }
    }
}