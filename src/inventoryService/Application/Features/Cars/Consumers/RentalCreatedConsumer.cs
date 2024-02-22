using Application.Services.Cars;
using Contracts.Events.Features.Rentals;
using Domain.Entities;
using Domain.Enums;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Consumers;
public class RentalCreatedConsumer : IConsumer<RentalCreatedEvent>
{
    private readonly ICarService _carService;

    public RentalCreatedConsumer(ICarService carsService)
    {
        _carService = carsService;
    }

    public async Task Consume(ConsumeContext<RentalCreatedEvent> context)
    {
        Car? car = await _carService.GetAsync(predicate: x => x.Id == context.Message.CarId);
        //await _carsService.CarShouldExistWhenSelected(car);
        if (car is not null)
        {
            car.CarState = CarState.Rented;
            await _carService.UpdateAsync(car);
        }
    }
}
