using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
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
    
    public static AuthResponseDto ToAuthResponse(string accessToken, string refreshToken)
    {
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

}