using Application.Features.Transmissions.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Transmissions.Rules;

public class TransmissionBusinessRules : BaseBusinessRules
{
    private readonly ITransmissionRepository _transmissionRepository;
    private readonly ILocalizationService _localizationService;

    public TransmissionBusinessRules(ITransmissionRepository transmissionRepository, ILocalizationService localizationService)
    {
        _transmissionRepository = transmissionRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, TransmissionsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task TransmissionShouldExistWhenSelected(Transmission? transmission)
    {
        if (transmission == null)
            await throwBusinessException(TransmissionsBusinessMessages.TransmissionNotExists);
    }

    public async Task TransmissionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Transmission? transmission = await _transmissionRepository.GetAsync(
            predicate: t => t.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TransmissionShouldExistWhenSelected(transmission);
    }
}