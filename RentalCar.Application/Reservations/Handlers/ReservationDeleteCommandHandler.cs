using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.Commands;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Reservations.Handlers;

public class ReservationDeleteCommandHandler(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ReservationDeleteCommand,Result<string>>
{
    public async Task<Result<string>> HandleAsync(ReservationDeleteCommand command)
    {
        var reservation = await reservationRepository.GetByIdAsync(command.Id);
        if (reservation == null)
        {
            return Result<string>.Fail("Reservation not found.", ErrorType.NotFound);
        }
        
        await reservationRepository.DeleteAsync(reservation);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null,"Reservation successfully deleted.");
    }
}