using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Auth.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IValidator<LoginCommand> validator,
    IHttpContextAccessor httpContextAccessor,
    IJwtTokenService jwtTokenService,
    IRefreshTokenRepository refreshRepo,
    IUnitOfWork unitOfWork )
    : ICommandHandler<LoginCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> HandleAsync(LoginCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(s => s));
            return Result<AuthResponseDto>.Fail(errors, ErrorType.Validation);
        }

        var user = await userRepository.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<AuthResponseDto>.Fail("Invalid email or password", ErrorType.Unauthorized);

        if (PasswordHasher.Verify(command.Password, user.PasswordHash))
            return Result<AuthResponseDto>.Fail("Invalid email or password", ErrorType.Unauthorized);

        var accessToken = jwtTokenService.GenerateToken(user);

        var refreshDb = jwtTokenService.GenerateRefreshToken(user);

        await refreshRepo.AddAsync(refreshDb);
        await unitOfWork.SaveChangesAsync();

        httpContextAccessor.HttpContext!.Response.Cookies.Append(
            "refreshToken",
            refreshDb.Token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = false, 
                SameSite = SameSiteMode.Strict,
                Expires = refreshDb.ExpiryDate
            });

        var response = AuthMappers.ToAuthResponse(accessToken, refreshDb.Token);
        
        return Result<AuthResponseDto>.Ok(response, "Login successful");
    }
}
