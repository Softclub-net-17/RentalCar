using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Reservations.Commands;

public class ReservationDeleteCommand(int id) :  ICommand<Result<string>>
{
    public int Id { get; set; } = id;
}