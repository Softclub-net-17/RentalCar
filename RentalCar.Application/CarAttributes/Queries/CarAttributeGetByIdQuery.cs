using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarAttributes.Queries;

public class CarAttributeGetByIdQuery(int id) : ICommand<Result<CarAttributeGetDto>>
{
    public int Id { get; set; } = id;
}