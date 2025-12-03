using RentalCar.Application.Cars.DTOs;

namespace RentalCar.Application.Users.DTOS;

public class UserReservationGetDto
{
    public List<CarGetDto> Cars { get; set; } = new();
}