using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Users.Commands;

public class UserDeleteCommand(int id) : ICommand<Result<string>>
{
    public int Id { get; set; } = id;
}