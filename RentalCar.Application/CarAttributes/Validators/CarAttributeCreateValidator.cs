
using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarAttributes.Validators;

public class CarAttributeCreateValidator : IValidator<CarAttributeCreateCommand>
{
    public ValidationResult Validate(CarAttributeCreateCommand instance)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.Name))
            result.AddError("Name is required");

        return result;
    }
}