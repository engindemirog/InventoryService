using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Cars.Queries.GetById;

public class GetByIdCarQuery : IRequest<GetByIdCarResponse>
{
    public Guid Id { get; set; }

    public class GetByIdCarQueryHandler : IRequestHandler<GetByIdCarQuery, GetByIdCarResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly CarBusinessRules _carBusinessRules;

        public GetByIdCarQueryHandler(IMapper mapper, ICarRepository carRepository, CarBusinessRules carBusinessRules)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
        }

        public async Task<GetByIdCarResponse> Handle(GetByIdCarQuery request, CancellationToken cancellationToken)
        {
            Car? car = await _carRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _carBusinessRules.CarShouldExistWhenSelected(car);

            GetByIdCarResponse response = _mapper.Map<GetByIdCarResponse>(car);
            return response;
        }
    }
}