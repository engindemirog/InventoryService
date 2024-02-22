using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICarRepository : IAsyncRepository<Car, Guid>, IRepository<Car, Guid>
{
}