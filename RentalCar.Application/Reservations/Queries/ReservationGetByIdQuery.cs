using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.DTOS;

namespace RentalCar.Application.Reservations.Queries;

public class ReservationGetByIdQuery(int id) :  IQuery<Result<ReservationGetDto>>
{
    public int Id { get; set; } = id;
}