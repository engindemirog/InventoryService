using Application.Features.Fuels.Constants;
using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Commands.Delete;

public class DeleteFuelCommand : IRequest<DeletedFuelResponse>
{
    public Guid Id { get; set; }

    public class DeleteFuelCommandHandler : IRequestHandler<DeleteFuelCommand, DeletedFuelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFuelRepository _fuelRepository;
        private readonly FuelBusinessRules _fuelBusinessRules;

        public DeleteFuelCommandHandler(IMapper mapper, IFuelRepository fuelRepository,
                                         FuelBusinessRules fuelBusinessRules)
        {
            _mapper = mapper;
            _fuelRepository = fuelRepository;
            _fuelBusinessRules = fuelBusinessRules;
        }

        public async Task<DeletedFuelResponse> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel? fuel = await _fuelRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _fuelBusinessRules.FuelShouldExistWhenSelected(fuel);

            await _fuelRepository.DeleteAsync(fuel!);

            DeletedFuelResponse response = _mapper.Map<DeletedFuelResponse>(fuel);
            return response;
        }
    }
}