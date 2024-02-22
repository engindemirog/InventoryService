using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Commands.Update;

public class UpdateFuelCommand : IRequest<UpdatedFuelResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelCommand, UpdatedFuelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFuelRepository _fuelRepository;
        private readonly FuelBusinessRules _fuelBusinessRules;

        public UpdateFuelCommandHandler(IMapper mapper, IFuelRepository fuelRepository,
                                         FuelBusinessRules fuelBusinessRules)
        {
            _mapper = mapper;
            _fuelRepository = fuelRepository;
            _fuelBusinessRules = fuelBusinessRules;
        }

        public async Task<UpdatedFuelResponse> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel? fuel = await _fuelRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _fuelBusinessRules.FuelShouldExistWhenSelected(fuel);
            fuel = _mapper.Map(request, fuel);

            await _fuelRepository.UpdateAsync(fuel!);

            UpdatedFuelResponse response = _mapper.Map<UpdatedFuelResponse>(fuel);
            return response;
        }
    }
}