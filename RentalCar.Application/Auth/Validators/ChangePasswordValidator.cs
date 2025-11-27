using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Validators;

public class ChangePasswordValidator : IValidator<ChangePasswordCommand>
{
    public ValidationResult Validate(ChangePasswordCommand instance)
    {
        var validationResult = new ValidationResult();

        if (instance.NewPassword.Length < 4)
            validationResult.AddError("New password too short");

        if (instance.NewPassword != instance.ConfirmPassword)
            validationResult.AddError("Passwords do not match");
        
        return validationResult;
    }
}