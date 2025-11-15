using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.DTOS;

namespace RentalCar.Application.Values.Queries;

public class ValueGetByIdQuery(int id) : IQuery<Result<ValueGetDto>>
{
    public int Id { get; set; } = id;
}