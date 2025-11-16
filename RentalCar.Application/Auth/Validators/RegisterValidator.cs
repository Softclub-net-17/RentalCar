using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Validators;

public class RegisterValidator: IValidator<RegisterCommand>
{
    public ValidationResult Validate(RegisterCommand instance)
    {
        var validationResult = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(instance.Email))
            validationResult.AddError("Email is required.");
        
        if (!instance.Email.Contains('@'))
            validationResult.AddError("Email is not in correct format.");
        
        if (string.IsNullOrWhiteSpace(instance.Password))
            validationResult.AddError("Password is required.");

        if (instance.Password != instance.ConfirmPassword)
            validationResult.AddError("Password and confirm password do not match.");
        
        if (instance.Password.Length < 6)
            validationResult.AddError("Password must be at least 6 characters.");
        
        return validationResult;
    }
}