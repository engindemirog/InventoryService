using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Cars;

public interface ICarService
{
    Task<Car?> GetAsync(
        Expression<Func<Car, bool>> predicate,
        Func<IQueryable<Car>, IIncludableQueryable<Car, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Car>?> GetListAsync(
        Expression<Func<Car, bool>>? predicate = null,
        Func<IQueryable<Car>, IOrderedQueryable<Car>>? orderBy = null,
        Func<IQueryable<Car>, IIncludableQueryable<Car, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Car> AddAsync(Car car);
    Task<Car> UpdateAsync(Car car);
    Task<Car> DeleteAsync(Car car, bool permanent = false);
}
