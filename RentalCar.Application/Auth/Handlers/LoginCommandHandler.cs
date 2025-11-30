using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IValidator<LoginCommand> validator,
    IHttpContextAccessor httpContextAccessor,
    IJwtTokenService jwtTokenService
) : ICommandHandler<LoginCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> HandleAsync(LoginCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<AuthResponseDto>.Fail(errors, ErrorType.Validation);
        }

        var user = await userRepository.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<AuthResponseDto>.Fail("Invalid email or password.", ErrorType.Unauthorized);

        var verify = PasswordHasher.Verify(command.Password, user.PasswordHash);
        if (!verify)
            return Result<AuthResponseDto>.Fail("Invalid email or password.", ErrorType.Unauthorized);

        var accessToken = jwtTokenService.GenerateToken(user);
        var refreshToken = jwtTokenService.GenerateRefreshToken(user);

        httpContextAccessor.HttpContext!.Response.Cookies.Append(
            "refreshToken",
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

        var response = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return Result<AuthResponseDto>.Ok(response, "Login successful.");
    }
}