using Application.Features.Transmissions.Constants;
using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.Delete;

public class DeleteTransmissionCommand : IRequest<DeletedTransmissionResponse>
{
    public Guid Id { get; set; }

    public class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, DeletedTransmissionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public DeleteTransmissionCommandHandler(IMapper mapper, ITransmissionRepository transmissionRepository,
                                         TransmissionBusinessRules transmissionBusinessRules)
        {
            _mapper = mapper;
            _transmissionRepository = transmissionRepository;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<DeletedTransmissionResponse> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission? transmission = await _transmissionRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _transmissionBusinessRules.TransmissionShouldExistWhenSelected(transmission);

            await _transmissionRepository.DeleteAsync(transmission!);

            DeletedTransmissionResponse response = _mapper.Map<DeletedTransmissionResponse>(transmission);
            return response;
        }
    }
}