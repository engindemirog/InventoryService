using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Cars;

public class CarManager : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly CarBusinessRules _carBusinessRules;

    public CarManager(ICarRepository carRepository, CarBusinessRules carBusinessRules)
    {
        _carRepository = carRepository;
        _carBusinessRules = carBusinessRules;
    }

    public async Task<Car?> GetAsync(
        Expression<Func<Car, bool>> predicate,
        Func<IQueryable<Car>, IIncludableQueryable<Car, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Car? car = await _carRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return car;
    }

    public async Task<IPaginate<Car>?> GetListAsync(
        Expression<Func<Car, bool>>? predicate = null,
        Func<IQueryable<Car>, IOrderedQueryable<Car>>? orderBy = null,
        Func<IQueryable<Car>, IIncludableQueryable<Car, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Car> carList = await _carRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return carList;
    }

    public async Task<Car> AddAsync(Car car)
    {
        Car addedCar = await _carRepository.AddAsync(car);

        return addedCar;
    }

    public async Task<Car> UpdateAsync(Car car)
    {
        Car updatedCar = await _carRepository.UpdateAsync(car);

        return updatedCar;
    }

    public async Task<Car> DeleteAsync(Car car, bool permanent = false)
    {
        Car deletedCar = await _carRepository.DeleteAsync(car);

        return deletedCar;
    }
}
