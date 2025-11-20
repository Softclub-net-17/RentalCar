using System.Text.Json.Serialization;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Reservations.Commands;

public class ReservationUpdateCommand : ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public DateTime? ReturnDate { get; set; }
}