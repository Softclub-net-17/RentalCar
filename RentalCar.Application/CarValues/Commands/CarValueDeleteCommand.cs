using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.CarValues.Commands;

public class CarValueDeleteCommand(int carId,int valueId) : ICommand<Result<string>>
{
    public int CarId { get; set; } = carId;
    public int ValueId { get; set; } =  valueId;
}