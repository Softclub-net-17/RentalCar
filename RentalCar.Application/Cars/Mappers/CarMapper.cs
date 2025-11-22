using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.Cars.Mappers
{
    public static class CarMapper
    {
        public static Car ToEntity(this CarCreateCommand command, string model, string make)
        {
            return new Car
            {
                Title = $"{make} {model}",
                PricePerHour = command.PricePerHour,
                Description = command.Description,
                Color = command.Color,
                Tinting = command.Tinting,
                Millage = command.Millage,
                Year = command.Year,
                Seats = command.Seats,
                ModelId = command.ModelId,
            };
        }

        public static void MapFrom(this CarUpdateCommand command, Car car)
        {
            car.PricePerHour = command.PricePerHour;
            car.Description = command.Description;
            car.Color = command.Color;
            car.Tinting = command.Tinting;
            car.Millage = command.Millage;
            car.Year = command.Year;
            car.Seats = command.Seats;
            car.ModelId = command.ModelId;
        }

        public static List<CarsGetDto> ToDto(this IEnumerable<Car> cars)
        {
            return cars.Select(c => new CarsGetDto()
            {
                Id = c.Id,
                Title = c.Title,
                PricePerHour = c.PricePerHour,
                Description = c.Description,
                Color= c.Color,
                Tinting = c.Tinting,
                Millage= c.Millage,
                Year = c.Year,
                Seats = c.Seats,
                ModelId = c.ModelId,
                Images = c.Images.Select(i => i.PhotoUrl).ToList() ?? new List<string>(),
                CarAttributes = c.CarValues.Select(cv => new CarAttributesGetDto
                {
                    AttributeId = cv.Value.CarAttribute.Id,
                    ValueId = cv.Value.Id,
                    AttributeName = cv.Value.CarAttribute.Name,
                    ValueName = cv.Value.Name
                }).ToList()
            }).ToList();
        }
    }
}
