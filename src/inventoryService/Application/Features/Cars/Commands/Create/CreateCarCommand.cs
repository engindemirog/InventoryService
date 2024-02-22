using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Domain.Enums;

namespace Application.Features.Cars.Commands.Create;

public class CreateCarCommand : IRequest<CreatedCarResponse>
{
    public Guid ModelId { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindexScore { get; set; }
    public CarState CarState { get; set; }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreatedCarResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly CarBusinessRules _carBusinessRules;

        public CreateCarCommandHandler(IMapper mapper, ICarRepository carRepository,
                                         CarBusinessRules carBusinessRules)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
        }

        public async Task<CreatedCarResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = _mapper.Map<Car>(request);

            await _carRepository.AddAsync(car);

            CreatedCarResponse response = _mapper.Map<CreatedCarResponse>(car);
            return response;
        }
    }
}