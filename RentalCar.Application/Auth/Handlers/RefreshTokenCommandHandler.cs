using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Auth.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class RefreshTokenCommandHandler(
    IJwtTokenService jwtService,
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    IUnitOfWork  unitOfWork )
    : ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> HandleAsync(RefreshTokenCommand command)
    {
        var storedRefresh = await refreshTokenRepository.GetByTokenAsync(command.RefreshToken);
        if (storedRefresh is null)
            return Result<AuthResponseDto>.Fail("Invalid refresh token", ErrorType.Unauthorized);

        if (storedRefresh.IsUsed)
            return Result<AuthResponseDto>.Fail("Refresh token already used", ErrorType.Unauthorized);

        if (storedRefresh.IsRevoked)
            return Result<AuthResponseDto>.Fail("Refresh token revoked", ErrorType.Unauthorized);

        if (storedRefresh.ExpiryDate <= DateTimeOffset.UtcNow)
            return Result<AuthResponseDto>.Fail("Refresh token expired", ErrorType.Unauthorized);

        var user = await userRepository.GetByIdAsync(storedRefresh.UserId);
        if (user is null)
            return Result<AuthResponseDto>.Fail("User not found", ErrorType.NotFound);

        storedRefresh.IsUsed = true;
        await refreshTokenRepository.UpdateAsync(storedRefresh);

        var newAccess = jwtService.GenerateToken(user);
        var newRefresh = jwtService.GenerateRefreshToken(user);

        await refreshTokenRepository.AddAsync(newRefresh);
        await unitOfWork.SaveChangesAsync();

        var dto = AuthMappers.ToAuthResponse(newAccess, newRefresh.Token);
        
        return Result<AuthResponseDto>.Ok(dto, "Tokens refreshed");
    }
}