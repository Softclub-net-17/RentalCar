using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class RefreshTokenCommandHandler(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository,
    IJwtTokenService jwtService)
    : ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> HandleAsync(RefreshTokenCommand command)
    {
        var ctx = httpContextAccessor.HttpContext!;
        var request = ctx.Request;
        
        if (!request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return Result<AuthResponseDto>.Fail("Refresh token missing.", ErrorType.Unauthorized);

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(refreshToken);
        var userId = int.Parse(jwt.Subject);

        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<AuthResponseDto>.Fail("User not found.", ErrorType.NotFound);

        var newAccessToken = jwtService.GenerateToken(user);
        var newRefreshToken = jwtService.GenerateRefreshToken(user);

        ctx.Response.Cookies.Append(
            "refreshToken",
            newRefreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

        var response = new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };

        return Result<AuthResponseDto>.Ok(response, "Token refreshed");
    }
}