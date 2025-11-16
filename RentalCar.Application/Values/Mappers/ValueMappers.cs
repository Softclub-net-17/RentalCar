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
    
    public static List<ValueGetDto> ToDto(this IEnumerable<Value> values)
    {
        return values.Select(value => new ValueGetDto()
        {
            Id = value.Id,
            Name = value.Name,
            CarAttributeId = value.CarAttributeId,
        }).ToList();
    }

    public static ValueGetDto ToDto(this Value value)
    {
        return new ValueGetDto
        {
            Id = value.Id,
            Name = value.Name,
            CarAttributeId = value.CarAttributeId,
        };
    }

    public static void MapFrom(this ValueUpdateCommand command, Value value)
    {
        value.Name = command.Name;
        value.CarAttributeId = command.CarAttributeId;
    }
}