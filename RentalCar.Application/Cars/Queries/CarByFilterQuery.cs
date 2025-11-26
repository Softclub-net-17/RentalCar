using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.ValueObject;

namespace RentalCar.Application.Cars.Queries;

public class CarByFilterQuery(CarFilter filter) : IQuery<Result<List<CarListItemDto>>>
{
    public CarFilter Filter { get; set; } = filter;
}