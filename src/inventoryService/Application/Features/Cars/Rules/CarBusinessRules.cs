using Application.Features.Cars.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Cars.Rules;

public class CarBusinessRules : BaseBusinessRules
{
    private readonly ICarRepository _carRepository;
    private readonly ILocalizationService _localizationService;

    public CarBusinessRules(ICarRepository carRepository, ILocalizationService localizationService)
    {
        _carRepository = carRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CarsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CarShouldExistWhenSelected(Car? car)
    {
        if (car == null)
            await throwBusinessException(CarsBusinessMessages.CarNotExists);
    }

    public async Task CarIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Car? car = await _carRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CarShouldExistWhenSelected(car);
    }
}