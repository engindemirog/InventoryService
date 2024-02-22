using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Transmissions;

public class TransmissionManager : ITransmissionService
{
    private readonly ITransmissionRepository _transmissionRepository;
    private readonly TransmissionBusinessRules _transmissionBusinessRules;

    public TransmissionManager(ITransmissionRepository transmissionRepository, TransmissionBusinessRules transmissionBusinessRules)
    {
        _transmissionRepository = transmissionRepository;
        _transmissionBusinessRules = transmissionBusinessRules;
    }

    public async Task<Transmission?> GetAsync(
        Expression<Func<Transmission, bool>> predicate,
        Func<IQueryable<Transmission>, IIncludableQueryable<Transmission, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Transmission? transmission = await _transmissionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return transmission;
    }

    public async Task<IPaginate<Transmission>?> GetListAsync(
        Expression<Func<Transmission, bool>>? predicate = null,
        Func<IQueryable<Transmission>, IOrderedQueryable<Transmission>>? orderBy = null,
        Func<IQueryable<Transmission>, IIncludableQueryable<Transmission, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Transmission> transmissionList = await _transmissionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return transmissionList;
    }

    public async Task<Transmission> AddAsync(Transmission transmission)
    {
        Transmission addedTransmission = await _transmissionRepository.AddAsync(transmission);

        return addedTransmission;
    }

    public async Task<Transmission> UpdateAsync(Transmission transmission)
    {
        Transmission updatedTransmission = await _transmissionRepository.UpdateAsync(transmission);

        return updatedTransmission;
    }

    public async Task<Transmission> DeleteAsync(Transmission transmission, bool permanent = false)
    {
        Transmission deletedTransmission = await _transmissionRepository.DeleteAsync(transmission);

        return deletedTransmission;
    }
}
