using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/auth")]
[ApiController]
public class AuthController(
    ICommandHandler<LoginCommand, Result<AuthResponseDto>> loginCommandHandler,
    ICommandHandler<RegisterCommand, Result<string>> registerCommandHandler,
    ICommandHandler<ChangePasswordCommand, Result<string>> changePasswordHandler,
    ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>> refreshHandler) 
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginCommand command)
    {
        var result = await loginCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        return Ok(result.Data);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return Unauthorized(new { error = "Refresh token missing" });

        var result = await refreshHandler.HandleAsync(new RefreshTokenCommand(refreshToken));

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterCommand command)
    {
        var result = await registerCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        return Ok(result.Message);
    }
    
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand command)
    {
        var result = await changePasswordHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Message);
    }
    
    private IActionResult HandleError<T>(Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.NotFound => NotFound(new { error = result.Message }),
            ErrorType.Validation => BadRequest(new { error = result.Message }),
            ErrorType.Conflict => Conflict(new { error = result.Message }),
            ErrorType.Internal => StatusCode(500, new { error = result.Message }),
            _ => StatusCode(500, new { error = result.Message ?? "Unhandled error" })
        };
    }
}