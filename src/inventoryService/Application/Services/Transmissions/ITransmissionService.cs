using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Transmissions;

public interface ITransmissionService
{
    Task<Transmission?> GetAsync(
        Expression<Func<Transmission, bool>> predicate,
        Func<IQueryable<Transmission>, IIncludableQueryable<Transmission, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Transmission>?> GetListAsync(
        Expression<Func<Transmission, bool>>? predicate = null,
        Func<IQueryable<Transmission>, IOrderedQueryable<Transmission>>? orderBy = null,
        Func<IQueryable<Transmission>, IIncludableQueryable<Transmission, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Transmission> AddAsync(Transmission transmission);
    Task<Transmission> UpdateAsync(Transmission transmission);
    Task<Transmission> DeleteAsync(Transmission transmission, bool permanent = false);
}
