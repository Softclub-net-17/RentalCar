using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Commands;

public class LoginCommand : ICommand<Result<AuthResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}