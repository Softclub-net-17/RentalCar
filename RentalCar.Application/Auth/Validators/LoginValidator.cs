using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Validators;

public class LoginValidator : IValidator<LoginCommand>
{
    public ValidationResult Validate(LoginCommand instance)
    {
        var validationResult = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.Email)) 
            validationResult.AddError("Email is required.");
        
        if (string.IsNullOrWhiteSpace(instance.Password))
            validationResult.AddError("Password is required.");
        
        return validationResult;
    }
}