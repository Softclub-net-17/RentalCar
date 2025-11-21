using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Cars.Handlers;

public class CarsGetByIdQueryHandler(ICarRepository carRepository) : IQueryHandler<CarsGetByIdQuery, Result<CarsGetDto>>
{
    public async Task<Result<CarsGetDto>> HandleAsync(CarsGetByIdQuery query)
    {
        var  car = await carRepository.GetByIdAsync(query.Id);
        if (car == null)
            return Result<CarsGetDto>.Fail("Car not found.", ErrorType.NotFound);

        var items = car.ToDto();
        
        return Result<CarsGetDto>.Ok(items);
    }
}