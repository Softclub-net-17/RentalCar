using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarAttributes.Commands;

public class CarAttributeCreateCommand : ICommand<Result<string>>
{
    public string Name { get; set; } = string.Empty;
}