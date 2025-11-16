using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.Commands;

namespace RentalCar.Application.Users.Validators;

public class UserUpdateValidator : IValidator<UserUpdateCommand>
{
    public ValidationResult Validate(UserUpdateCommand instance)
    {
        var validationResult = new ValidationResult();

        if (instance.Id <= 0)
            validationResult.AddError("Id is required.");

        if (string.IsNullOrWhiteSpace(instance.Email))
            validationResult.AddError("Email is required.");
        
        if (!instance.Email.Contains('@'))
            validationResult.AddError("Email is not in correct format.");

        if (string.IsNullOrWhiteSpace(instance.Password))
            validationResult.AddError("Password is required.");

        if (instance.Password.Length < 6)
            validationResult.AddError("Password must be at least 6 characters.");

        return validationResult;
    }
}