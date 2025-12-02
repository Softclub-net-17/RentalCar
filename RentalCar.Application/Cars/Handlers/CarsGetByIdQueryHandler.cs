using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Cars.Handlers;

public class CarsGetByIdQueryHandler(
    ICarRepository carRepository,
    IReservationRepository reservationRepository )
    : IQueryHandler<CarGetByIdQuery, Result<CarGetDto>>
{
    public async Task<Result<CarGetDto>> HandleAsync(CarGetByIdQuery query)
    {
        var car = await carRepository.GetByIdAsync(query.Id);
        if (car == null)
            return Result<CarGetDto>.Fail("Car not found.", ErrorType.NotFound);

        var item = await car.ToDto(reservationRepository);

        return Result<CarGetDto>.Ok(item);
    }
}