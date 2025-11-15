using RentalCar.Application.Values.Commands;
using RentalCar.Application.Values.DTOS;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.Values.Mappers;

public static class ValueMappers
{
    public static Value ToEntity(this ValueCreateCommand command)
    {
        return new Value
        {
            Name = command.Name,
            CarAttributeId = command.CarAttributeId,
        };
    }
    
    public static List<ValueGetDto> ToDto(IEnumerable<Value> carAttributes)
    {
        return carAttributes.Select(car => new ValueGetDto()
        {
            Id = car.Id,
            Name = car.Name,
            CarAttributeId = car.CarAttributeId,
        }).ToList();
    }

    public static ValueGetDto ToDto(Value car)
    {
        return new ValueGetDto
        {
            Id = car.Id,
            Name = car.Name,
            CarAttributeId = car.CarAttributeId,
        };
    }

    public static void MapFrom(this ValueUpdateCommand command, Value car)
    {
        car.Name = command.Name;
        car.CarAttributeId = command.CarAttributeId;
    }
}