using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Queries.GetById;

public class GetByIdTransmissionQuery : IRequest<GetByIdTransmissionResponse>
{
    public Guid Id { get; set; }

    public class GetByIdTransmissionQueryHandler : IRequestHandler<GetByIdTransmissionQuery, GetByIdTransmissionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public GetByIdTransmissionQueryHandler(IMapper mapper, ITransmissionRepository transmissionRepository, TransmissionBusinessRules transmissionBusinessRules)
        {
            _mapper = mapper;
            _transmissionRepository = transmissionRepository;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<GetByIdTransmissionResponse> Handle(GetByIdTransmissionQuery request, CancellationToken cancellationToken)
        {
            Transmission? transmission = await _transmissionRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _transmissionBusinessRules.TransmissionShouldExistWhenSelected(transmission);

            GetByIdTransmissionResponse response = _mapper.Map<GetByIdTransmissionResponse>(transmission);
            return response;
        }
    }
}