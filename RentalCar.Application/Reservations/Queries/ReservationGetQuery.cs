using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.DTOS;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.Reservations.Queries;

public class ReservationGetQuery : IQuery<Result<List<ReservationGetDto>>>
{
    
}