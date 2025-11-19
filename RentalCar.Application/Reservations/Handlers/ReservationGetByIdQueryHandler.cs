using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.DTOS;
using RentalCar.Application.Reservations.Mappers;
using RentalCar.Application.Reservations.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Reservations.Handlers;

public class ReservationGetByIdQueryHandler(
    IReservationRepository repository) 
    : IQueryHandler<ReservationGetByIdQuery, Result<ReservationGetDto>>
{
    public async Task<Result<ReservationGetDto>> HandleAsync(ReservationGetByIdQuery query)
    {
        var  reservation = await repository.GetByIdAsync(query.Id);
        if (reservation == null)
            return Result<ReservationGetDto>.Fail("Reservation not found.", ErrorType.NotFound);

        var items = reservation.ToDto();
        
        return Result<ReservationGetDto>.Ok(items);
    }
}