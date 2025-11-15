using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarAttributes.Validators;

public class CarAttributeUpdateValidator : IValidator<CarAttributeUpdateCommand>
{
    public ValidationResult Validate(CarAttributeUpdateCommand instance)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.Name))
            result.AddError("Name is required");
        
        if (instance.Id <= 0)
            result.AddError("Id is required");  

        return result;
    }
}