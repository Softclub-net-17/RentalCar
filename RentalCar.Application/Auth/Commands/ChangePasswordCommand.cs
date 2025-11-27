using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Commands;

public class ChangePasswordCommand : ICommand<Result<string>>
{
    public string OldPassword { get; set; } = String.Empty;
    public string NewPassword { get; set; } = String.Empty;
    public string ConfirmPassword { get; set; } = String.Empty;
}