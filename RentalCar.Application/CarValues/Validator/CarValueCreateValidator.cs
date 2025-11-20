using RentalCar.Application.CarValues.Commands;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Entities;
using ValidationResult = RentalCar.Application.Common.Validations.ValidationResult;

namespace RentalCar.Application.CarValues.Validator;

public class CarValueCreateValidator : IValidator<CarValueCreateCommand>
{
    public ValidationResult Validate(CarValueCreateCommand instance)
    {
        var result = new ValidationResult();

        if (instance.CarId <= 0)
            result.AddError("Id is required");

        if (instance.ValueId <= 0)
            result.AddError("Id is required"); 
        
        return result;
    }
}