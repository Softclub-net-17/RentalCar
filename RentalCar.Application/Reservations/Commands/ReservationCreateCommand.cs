using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Reservations.Commands;

public class ReservationCreateCommand : ICommand<Result<string>>
{
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}