using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.Commands;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Reservations.Handlers;

public class ReservationUpdateCommandHandler(
    IReservationRepository reservationRepository,
    ICarRepository carRepository,
    IUnitOfWork unitOfWork,
    IValidator<ReservationUpdateCommand> validator)
    : ICommandHandler<ReservationUpdateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ReservationUpdateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var reservation = await reservationRepository.GetByIdAsync(command.Id);
        if (reservation is null)
            return Result<string>.Fail("Reservation not found.", ErrorType.NotFound);

        if (command.ReturnDate is null)
            return Result<string>.Fail("Return date is required.", ErrorType.Validation);

        var returnDate = command.ReturnDate.Value;

        if (returnDate <= reservation.EndDate)
        {
            reservation.ReturnDate = returnDate;
            await reservationRepository.UpdateAsync(reservation);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null, "Car returned on time. No extra charges.");
        }

        var extraHours = (returnDate - reservation.EndDate).TotalHours;

        var car = await carRepository.GetByIdAsync(reservation.CarId);
        if (car is null)
            return Result<string>.Fail("Car not found.", ErrorType.NotFound);

        var extraCost = (decimal)extraHours * car.PricePerHour;
        
        extraHours = Math.Round(extraHours, 0);

        reservation.TotalPrice += extraCost;
        reservation.ReturnDate = returnDate;
        reservation.EndDate = returnDate;

        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null,$"Late return: {extraCost:F0} added.");
    }
}
