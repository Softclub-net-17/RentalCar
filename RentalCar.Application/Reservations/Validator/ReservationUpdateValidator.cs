using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.Commands;
using ValidationResult = RentalCar.Application.Common.Validations.ValidationResult;

namespace RentalCar.Application.Reservations.Validator;

public class ReservationUpdateValidator : IValidator<ReservationUpdateCommand>
{
    public ValidationResult Validate(ReservationUpdateCommand instance)
    {
        var result = new ValidationResult();
        
        if (instance.Id <= 0)
            result.AddError("Reservation Id is required.");

        if (instance.ReturnDate is null)
            result.AddError("ReturnDate is required.");
        
        return result;
    }
}