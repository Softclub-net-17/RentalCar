using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.DTOS;
using RentalCar.Application.Users.Mappers;
using RentalCar.Application.Users.Queries;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Users.Handlers;

public class UserGetMeReservationQueryHandler(
    IUserRepository userRepository,
    IReservationRepository reservationRepository,
    ICarRepository carRepository,
    IHttpContextAccessor httpContext
) : IQueryHandler<UserGetMeReservationQuery, Result<UserReservationGetDto>>
{
    public async Task<Result<UserReservationGetDto>> HandleAsync(UserGetMeReservationQuery query)
    {
        var userIdClaim = httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
            return Result<UserReservationGetDto>.Fail("Unauthorized", ErrorType.Unauthorized);

        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
            return Result<UserReservationGetDto>.Fail("User not found", ErrorType.NotFound);

        var dto = user.ToReservationDto();

        var reservations = await reservationRepository.GetByUserIdAsync(userId);
        reservations = reservations.OrderByDescending(r => r.StartDate).ToList();

        var carIds = reservations
            .Select(r => r.CarId)
            .Distinct()
            .ToList();

        var cars = new List<Car>();

        foreach (var carId in carIds)
        {
            var car = await carRepository.GetByIdAsync(carId);
            if (car != null)
                cars.Add(car);
        }

        dto.Cars = await cars.ToDto(reservationRepository);

        return Result<UserReservationGetDto>.Ok(dto);
    }
}