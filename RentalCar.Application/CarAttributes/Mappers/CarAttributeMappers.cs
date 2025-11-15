using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.CarAttributes.Mappers;

public static class CarAttributeMappers
{
    public static CarAttribute ToEntity(this CarAttributeCreateCommand command)
    {
        return new CarAttribute
        {
            Name = command.Name,
        };
    }
    
    public static List<CarAttributeGetDto> ToDto(this IEnumerable<CarAttribute> carAttributes)
    {
        return carAttributes.Select(car => new CarAttributeGetDto()
        {
            Id = car.Id,
            Name = car.Name
        }).ToList();
    }

    public static CarAttributeGetDto ToDto(this CarAttribute car)
    {
        return new CarAttributeGetDto
        {
            Id = car.Id,
            Name = car.Name
        };
    }

    public static void MapFrom(this CarAttributeUpdateCommand command, CarAttribute carAttribute)
    {
        carAttribute.Name = command.Name;
    }
}