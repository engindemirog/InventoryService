using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Commands.Create;

public class CreateFuelCommand : IRequest<CreatedFuelResponse>
{
    public string Name { get; set; }

    public class CreateFuelCommandHandler : IRequestHandler<CreateFuelCommand, CreatedFuelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFuelRepository _fuelRepository;
        private readonly FuelBusinessRules _fuelBusinessRules;

        public CreateFuelCommandHandler(IMapper mapper, IFuelRepository fuelRepository,
                                         FuelBusinessRules fuelBusinessRules)
        {
            _mapper = mapper;
            _fuelRepository = fuelRepository;
            _fuelBusinessRules = fuelBusinessRules;
        }

        public async Task<CreatedFuelResponse> Handle(CreateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel fuel = _mapper.Map<Fuel>(request);

            await _fuelRepository.AddAsync(fuel);

            CreatedFuelResponse response = _mapper.Map<CreatedFuelResponse>(fuel);
            return response;
        }
    }
}