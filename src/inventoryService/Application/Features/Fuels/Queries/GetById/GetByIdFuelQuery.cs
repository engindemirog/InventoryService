using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Queries.GetById;

public class GetByIdFuelQuery : IRequest<GetByIdFuelResponse>
{
    public Guid Id { get; set; }

    public class GetByIdFuelQueryHandler : IRequestHandler<GetByIdFuelQuery, GetByIdFuelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFuelRepository _fuelRepository;
        private readonly FuelBusinessRules _fuelBusinessRules;

        public GetByIdFuelQueryHandler(IMapper mapper, IFuelRepository fuelRepository, FuelBusinessRules fuelBusinessRules)
        {
            _mapper = mapper;
            _fuelRepository = fuelRepository;
            _fuelBusinessRules = fuelBusinessRules;
        }

        public async Task<GetByIdFuelResponse> Handle(GetByIdFuelQuery request, CancellationToken cancellationToken)
        {
            Fuel? fuel = await _fuelRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _fuelBusinessRules.FuelShouldExistWhenSelected(fuel);

            GetByIdFuelResponse response = _mapper.Map<GetByIdFuelResponse>(fuel);
            return response;
        }
    }
}