using RentalCar.Application.Auth.Commands;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Enums;

namespace RentalCar.Application.Auth.Mappers;

public static class AuthMappers
{
    public static User ToEntity(this RegisterCommand command, string passwordHash)
    {
        return new User()
        {
            Email = command.Email,
            Role = Role.User,
            PasswordHash = passwordHash,
        };
    }
}