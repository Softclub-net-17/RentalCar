using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Validators;

public class LoginValidator : IValidator<LoginCommand>
{
    public ValidationResult Validate(LoginCommand instance)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.Email))
            result.AddError("Email is required.");

        if (!instance.Email.Contains('@'))
            result.AddError("Email format is invalid.");

        if (string.IsNullOrWhiteSpace(instance.Password))
            result.AddError("Password is required.");

        if (instance.Password.Length < 6)
            result.AddError("Password must be at least 6 characters.");

        return result;
    }
}