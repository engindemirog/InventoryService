using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Domain.Enums;

namespace Application.Features.Cars.Commands.Update;

public class UpdateCarCommand : IRequest<UpdatedCarResponse>
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindexScore { get; set; }
    public CarState CarState { get; set; }

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, UpdatedCarResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly CarBusinessRules _carBusinessRules;

        public UpdateCarCommandHandler(IMapper mapper, ICarRepository carRepository,
                                         CarBusinessRules carBusinessRules)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
        }

        public async Task<UpdatedCarResponse> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Car? car = await _carRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _carBusinessRules.CarShouldExistWhenSelected(car);
            car = _mapper.Map(request, car);

            await _carRepository.UpdateAsync(car!);

            UpdatedCarResponse response = _mapper.Map<UpdatedCarResponse>(car);
            return response;
        }
    }
}