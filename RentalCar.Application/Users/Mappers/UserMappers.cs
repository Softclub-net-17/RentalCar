using RentalCar.Application.Users.Commands;
using RentalCar.Application.Users.DTOS;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.Users.Mappers;

public static class UserMappers
{
    public static void MapToEntity(this UserUpdateCommand command, User user, string hashedPassword)
    {
        user.Email = command.Email;
        user.PasswordHash = hashedPassword;
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
}