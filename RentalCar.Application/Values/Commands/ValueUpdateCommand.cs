using System.Text.Json.Serialization;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Values.Commands;

public class ValueUpdateCommand : ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CarAttributeId { get; set; }
}