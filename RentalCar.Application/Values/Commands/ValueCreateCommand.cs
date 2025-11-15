using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Values.Commands;

public class ValueCreateCommand : ICommand<Result<string>>
{
    public string Name { get; set; } = string.Empty;
    public int CarAttributeId { get; set; }
}