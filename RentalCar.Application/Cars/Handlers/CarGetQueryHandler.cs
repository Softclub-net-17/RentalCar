using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using RentalCar.Application.Cars.Mappers;

namespace RentalCar.Application.Cars.Handlers;

public class CarGetQueryHandler(
    ICarRepository carRepository,
    IReservationRepository reservationRepository )
    : IQueryHandler<CarGetQuery, Result<List<CarGetDto>>>
{
    public async Task<Result<List<CarGetDto>>> HandleAsync(CarGetQuery query)
    {
        var cars = await carRepository.GetAllAsync();

        var items = await cars.ToDto(reservationRepository);

        return Result<List<CarGetDto>>.Ok(items);
    }
}