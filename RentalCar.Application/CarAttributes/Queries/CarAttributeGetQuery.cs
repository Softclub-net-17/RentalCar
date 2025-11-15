using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.CarAttributes.Queries;

public class CarAttributeGetQuery : ICommand<Result<List<CarAttributeGetDto>>>
{
    
}