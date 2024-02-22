using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.Update;

public class UpdateTransmissionCommand : IRequest<UpdatedTransmissionResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public class UpdateTransmissionCommandHandler : IRequestHandler<UpdateTransmissionCommand, UpdatedTransmissionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public UpdateTransmissionCommandHandler(IMapper mapper, ITransmissionRepository transmissionRepository,
                                         TransmissionBusinessRules transmissionBusinessRules)
        {
            _mapper = mapper;
            _transmissionRepository = transmissionRepository;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<UpdatedTransmissionResponse> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission? transmission = await _transmissionRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _transmissionBusinessRules.TransmissionShouldExistWhenSelected(transmission);
            transmission = _mapper.Map(request, transmission);

            await _transmissionRepository.UpdateAsync(transmission!);

            UpdatedTransmissionResponse response = _mapper.Map<UpdatedTransmissionResponse>(transmission);
            return response;
        }
    }
}