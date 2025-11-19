using RentalCar.Application.CarValues.Commands;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.CarValues.Mappers;

public static class CarValueMappers
{
    public static CarValue ToEntity(this CarValueCreateCommand command)
    {
        return new CarValue
        {
            CarId = command.CarId,
            ValueId = command.ValueId,
        };
    }
    
    public static void MapFrom(this CarValueUpdateCommand command, CarValue carValue)
    {
        carValue.CarId = command.CarId;
        carValue.ValueId = command.ValueId;
    }
}