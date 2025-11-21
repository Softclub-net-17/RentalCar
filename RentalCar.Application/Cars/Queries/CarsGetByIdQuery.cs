using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Cars.Queries;

public class CarsGetByIdQuery(int id) : IQuery<Result<CarsGetDto>>
{
    public int Id { get; set; } = id;
}