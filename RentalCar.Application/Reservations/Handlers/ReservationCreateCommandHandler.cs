using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.Commands;
using RentalCar.Application.Reservations.Mappers;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Reservations.Handlers;

public class ReservationCreateCommandHandler(
    IReservationRepository reservationRepository,
    ICarRepository carRepository,
    IUnitOfWork unitOfWork,
    IValidator<ReservationCreateCommand> validator)
    : ICommandHandler<ReservationCreateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ReservationCreateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }

        var car = await carRepository.GetByIdAsync(command.CarId);
        if (car is null)
            return Result<string>.Fail("Car not found.", ErrorType.NotFound);
        
        var busy = await reservationRepository.IsCarBusy(
            command.CarId,
            command.StartDate,
            command.EndDate
        );

        if (busy)
            return Result<string>.Fail("Car is already reserved for the selected dates.", ErrorType.Conflict);

        var reservation = command.ToEntity(car);

        await reservationRepository.CreateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null,$"Reservation created successfully. Total price: {reservation.TotalPrice}.");
    }
}