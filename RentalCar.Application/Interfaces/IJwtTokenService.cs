using RentalCar.Domain.Entities;

namespace RentalCar.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    RefreshToken GenerateRefreshToken(User user);
}
