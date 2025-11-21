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
        
        public static List<CarValue> ToCarValues(this CarCreateCommand command, int carId)
        {
            return command.ValueIds.Select(valueId => new CarValue
            {
                CarId = carId,
                ValueId = valueId
            }).ToList();
        }
        
        public static List<CarValue> ToCarValues(this CarUpdateCommand command, int carId)
        {
            return command.ValueIds.Select(valueId => new CarValue
            {
                CarId = carId,
                ValueId = valueId
            }).ToList();
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

        public static List<CarGetDto> ToDto(this IEnumerable<Car> cars)
        {
            return cars.Select(c => new CarGetDto()
            {
                Id = c.Id,
                Title = c.Title,
                PricePerHour = c.PricePerHour,
                Description = c.Description,
                Color = c.Color,
                Tinting = c.Tinting,
                Millage = c.Millage,
                Year = c.Year,
                Seats = c.Seats,
                ModelId = c.ModelId,
                Images = c.Images.Select(i => i.PhotoUrl).ToList(),
                Values = c.CarValues.Select(v => v.Value.Name).ToList()
            }).ToList();
        }


        public static CarGetDto ToDto(this Car car)
        {
            return new CarGetDto()
            {
                Id = car.Id,
                Title = car.Title,
                PricePerHour = car.PricePerHour,
                Description = car.Description,
                Color = car.Color,
                Tinting = car.Tinting,
                Millage = car.Millage,
                Year = car.Year,
                Seats = car.Seats,
                ModelId = car.ModelId,
                Images = car.Images.Select(i => i.PhotoUrl).ToList(),
                Values = car.CarValues.Select(v => v.Value.Name).ToList()
            };
        }
    }
}
