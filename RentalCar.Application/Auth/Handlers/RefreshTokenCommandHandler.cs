using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class RefreshTokenCommandHandler(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository,
    IJwtTokenService jwtService)
    : ICommandHandler<RefreshTokenCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(RefreshTokenCommand command)
    {
        var ctx = httpContextAccessor.HttpContext!; 
        var request = ctx.Request;
        var response = ctx.Response;

        // 1. Получаем refresh token из cookie
        if (!request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return Result<string>.Fail("Refresh token missing.", ErrorType.Unauthorized);

        // 2. Если refresh-токен у тебя JWT → достаём userId
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(refreshToken);
        var userId = int.Parse(jwt.Subject);

        // 3. Находим пользователя
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<string>.Fail("User not found.", ErrorType.NotFound);

        // 4. Генерируем новые токены
        var newAccessToken = jwtService.GenerateToken(user);
        var newRefreshToken = jwtService.GenerateRefreshToken(user); // лучше тоже JWT

        // 5. Обновляем refresh token в cookie
        response.Cookies.Append(
            "refreshToken",
            newRefreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // локально
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

        return Result<string>.Ok(newAccessToken, "Token refreshed");
    }
}
