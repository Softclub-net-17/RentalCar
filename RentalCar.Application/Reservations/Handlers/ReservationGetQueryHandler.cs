using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.DTOS;
using RentalCar.Application.Reservations.Mappers;
using RentalCar.Application.Reservations.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Reservations.Handlers;

public class ReservationGetQueryHandler(
    IReservationRepository reservationRepository)
    : IQueryHandler<ReservationGetQuery, Result<List<ReservationGetDto>>>
{
    public async Task<Result<List<ReservationGetDto>>> HandleAsync(ReservationGetQuery query)
    {
        var reservations = await reservationRepository.GetAllAsync();
        var items = reservations.ToDto();
        
        return Result<List<ReservationGetDto>>.Ok(items);
    }
}