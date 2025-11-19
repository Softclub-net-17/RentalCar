using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarValues.Commands;

public class CarValueCreateCommand : ICommand<Result<string>>
{
    public int CarId { get; set; }
    public int ValueId { get; set; }
}