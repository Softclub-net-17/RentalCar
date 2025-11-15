using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.Commands;

namespace RentalCar.Application.Values.Validators;

public class ValueCreateValidator: IValidator<ValueCreateCommand>
{
    public ValidationResult Validate(ValueCreateCommand instance)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.Name))
            result.AddError("Name is required");

        if (instance.CarAttributeId <= 0)
            result.AddError("CarAttributeId is required"); 
        
        return result;
    }
}