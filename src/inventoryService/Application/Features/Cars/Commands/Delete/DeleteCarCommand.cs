using Application.Features.Cars.Constants;
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Cars.Commands.Delete;

public class DeleteCarCommand : IRequest<DeletedCarResponse>
{
    public Guid Id { get; set; }

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, DeletedCarResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly CarBusinessRules _carBusinessRules;

        public DeleteCarCommandHandler(IMapper mapper, ICarRepository carRepository,
                                         CarBusinessRules carBusinessRules)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
        }

        public async Task<DeletedCarResponse> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Car? car = await _carRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _carBusinessRules.CarShouldExistWhenSelected(car);

            await _carRepository.DeleteAsync(car!);

            DeletedCarResponse response = _mapper.Map<DeletedCarResponse>(car);
            return response;
        }
    }
}