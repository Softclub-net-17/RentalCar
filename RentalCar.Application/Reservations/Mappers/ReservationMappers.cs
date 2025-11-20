using RentalCar.Application.Reservations.Commands;
using RentalCar.Application.Reservations.DTOS;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.Reservations.Mappers;

public static class ReservationMappers
{
    public static List<ReservationGetDto> ToDto(this IEnumerable<Reservation> reservations)
    {
        return reservations.Select(reservation => new ReservationGetDto()
        {
            Id = reservation.Id,
            CarId = reservation.CarId,
            UserId = reservation.UserId,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate,
            TotalPrice = reservation.TotalPrice,
            ReturnDate = reservation.ReturnDate,
        }).ToList();
    }

    public static ReservationGetDto ToDto(this Reservation reservation)
    {
        return new ReservationGetDto()
        {
            Id = reservation.Id,
            CarId = reservation.CarId,
            UserId = reservation.UserId,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate,
            TotalPrice = reservation.TotalPrice,
            ReturnDate = reservation.ReturnDate,
        };
    }
    
    public static Reservation ToEntity(this ReservationCreateCommand command, Car car)
    {
        var hours = (command.EndDate - command.StartDate).TotalHours;

        return new Reservation
        {
            UserId = command.UserId,
            CarId = command.CarId,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            TotalPrice = (decimal)hours * car.PricePerHour
        };
    }

}