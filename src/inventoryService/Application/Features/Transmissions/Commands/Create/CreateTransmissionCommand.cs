using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.Create;

public class CreateTransmissionCommand : IRequest<CreatedTransmissionResponse>
{
    public string Name { get; set; }

    public class CreateTransmissionCommandHandler : IRequestHandler<CreateTransmissionCommand, CreatedTransmissionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public CreateTransmissionCommandHandler(IMapper mapper, ITransmissionRepository transmissionRepository,
                                         TransmissionBusinessRules transmissionBusinessRules)
        {
            _mapper = mapper;
            _transmissionRepository = transmissionRepository;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<CreatedTransmissionResponse> Handle(CreateTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission transmission = _mapper.Map<Transmission>(request);

            await _transmissionRepository.AddAsync(transmission);

            CreatedTransmissionResponse response = _mapper.Map<CreatedTransmissionResponse>(transmission);
            return response;
        }
    }
}