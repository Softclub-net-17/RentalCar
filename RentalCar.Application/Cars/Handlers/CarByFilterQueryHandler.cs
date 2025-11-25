using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Cars.Handlers;

public class CarByFilterQueryHandler(
    ICarRepository carRepository) 
    : IQueryHandler<CarByFilterQuery, Result<List<CarListItemDto>>>
{

    public async Task<Result<List<CarListItemDto>>> HandleAsync(CarByFilterQuery request)
    {
        var cars = await carRepository.GetByFilterAsync(request.Filter);
        var items = cars.ToFilter();
        return Result<List<CarListItemDto>>.Ok(items);
    }
}