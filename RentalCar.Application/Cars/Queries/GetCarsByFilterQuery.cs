using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Cars.Queries;

public class GetCarsByFilterQuery(CarFilterGetDto filter) : IQuery<Result<List<CarListItemDto>>>
{
    public CarFilterGetDto Filter { get; set; } = filter;
}