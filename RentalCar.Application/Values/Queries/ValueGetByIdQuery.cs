using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.DTOS;

namespace RentalCar.Application.Values.Queries;

public class ValueGetByIdQuery(int id) : ICommand<Result<ValueGetDto>>
{
    public int Id { get; set; } = id;
}