using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.Commands;
using ValidationResult = RentalCar.Application.Common.Validations.ValidationResult;

namespace RentalCar.Application.Reservations.Validator;

public class ReservationCreateValidator : IValidator<ReservationCreateCommand>
{
    public ValidationResult Validate(ReservationCreateCommand instance)
    {
        var result = new ValidationResult();
        
        if (instance.UserId <= 0)
            result.AddError("UserId is required");

        if (instance.CarId <= 0)
            result.AddError("CarId is required");

        if (instance.StartDate == default)
            result.AddError("StartDate is required");

        if (instance.EndDate == default)
            result.AddError("EndDate is required");

        if (instance.EndDate <= instance.StartDate)
            result.AddError("EndDate must be greater than StartDate");
        
        return result;
    }
}