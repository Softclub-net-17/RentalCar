using System.Text.Json.Serialization;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarAttributes.Commands;

public class CarAttributeUpdateCommand : ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}