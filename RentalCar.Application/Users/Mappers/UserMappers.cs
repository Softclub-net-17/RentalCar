using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Reservations.DTOS;
using RentalCar.Application.Users.Commands;
using RentalCar.Application.Users.DTOS;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Users.Mappers;

public static class UserMappers
{
    public static void MapToEntity(this UserUpdateCommand command, User user)
    {
        user.Email = command.Email;
        user.Role = command.Role;
    }
    
    public static UserGetDto ToDto(this User user)
    {
        return new UserGetDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        };
    }

    public static List<UserGetDto> ToDto(this IEnumerable<User> users)
    {
        return users.Select(user => new UserGetDto()
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        }).ToList();
    }
    public static void MapFrom(this User user, string passwordHash)
    {
        user.PasswordHash = passwordHash;
    }
    
    public static async Task<List<CarGetDto>> ToDto(this IEnumerable<Car> cars, IReservationRepository reservationRepository)
    {
        var result = new List<CarGetDto>();

        foreach (var c in cars)
        {
            var reservations = await reservationRepository.GetMeActiveByCarIdAsync(c.Id);

            result.Add(new CarGetDto
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
                Images = c.Images.Select(i => i.PhotoUrl).ToList() ?? new List<string>(),
                CarAttributes = c.CarValues.Select(cv => new CarAttributesGetDto
                {
                    AttributeId = cv.Value.CarAttribute.Id,
                    ValueId = cv.Value.Id,
                    AttributeName = cv.Value.CarAttribute.Name,
                    ValueName = cv.Value.Name
                }).ToList(),
                BusyDates = reservations.ToBusyDatesDto()
            });
        }

        return result;
    }
    
    public static UserReservationGetDto ToReservationDto(this User user)
    {
        return new UserReservationGetDto
        {
            Cars = new List<CarGetDto>()
        };
    }
}