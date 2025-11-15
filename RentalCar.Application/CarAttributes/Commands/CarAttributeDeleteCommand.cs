using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarAttributes.Commands;

public class CarAttributeDeleteCommand(int id) : ICommand<Result<string>>
{
    public int Id { get; set; } = id;
}