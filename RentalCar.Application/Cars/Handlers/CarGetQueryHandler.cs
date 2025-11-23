using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Cars.Handlers
{
    public class CarGetQueryHandler
        (ICarRepository carRepository) : IQueryHandler<CarGetQuery, Result<List<CarGetDto>>>
    {
        public async Task<Result<List<CarGetDto>>> HandleAsync(CarGetQuery query)
        {
            var cars = await carRepository.GetAllAsync();
            var items = cars.ToDto();

            return Result<List<CarGetDto>>.Ok(items);
        }
    }
}
