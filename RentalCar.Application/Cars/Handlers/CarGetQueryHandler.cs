using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.DTOs;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
