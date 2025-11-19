using RentalCar.Application.CarValues.Commands;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.Commands;
using ValidationResult = RentalCar.Application.Common.Validations.ValidationResult;

namespace RentalCar.Application.CarValues.Validator;

public class CarValueUpdateValidator : IValidator<CarValueUpdateCommand>
{
    public ValidationResult Validate(CarValueUpdateCommand instance)
    {
        var result = new ValidationResult();

        if (instance.CarId <= 0)
            result.AddError("Id is required");

        if (instance.ValueId <= 0)
            result.AddError("Id is required"); 
        
        return result;
    }
}