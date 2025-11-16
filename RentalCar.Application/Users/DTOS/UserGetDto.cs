using RentalCar.Domain.Enums;

namespace RentalCar.Application.Users.DTOS;

public class UserGetDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
}