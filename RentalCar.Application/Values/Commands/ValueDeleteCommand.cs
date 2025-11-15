using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Values.Commands;

public class ValueDeleteCommand(int id) : ICommand<Result<string>>
{
    public int Id { get; set; }
}