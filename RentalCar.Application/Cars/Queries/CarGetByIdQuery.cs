using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Cars.Queries;

public class CarGetByIdQuery(int id) : IQuery<Result<CarGetDto>>
{
    public int Id { get; set; } = id;
}