using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.DTOS;

namespace RentalCar.Application.Users.Queries;

public class UserGetByIdQuery(int id) : IQuery<Result<UserGetDto>>
{
    public int Id { get; set; } = id;
}