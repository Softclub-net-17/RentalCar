using System.Text.Json.Serialization;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Enums;

namespace RentalCar.Application.Users.Commands;

public class UserUpdateCommand : ICommand<Result<string>>
{
    [JsonIgnore] public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Role Role { get; set; }
}