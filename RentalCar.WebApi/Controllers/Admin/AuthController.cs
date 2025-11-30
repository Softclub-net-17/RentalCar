using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Admin;

[ApiExplorerSettings(GroupName = "admin")]
[Route("api/admin/auth")]
[ApiController]
public class AuthController(
    ICommandHandler<LoginCommand, Result<AuthResponseDto>> loginCommandHandler
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginCommand command)
    {
        var result = await loginCommandHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync(
        [FromServices] ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>> refreshHandler)
    {
        var result = await refreshHandler.HandleAsync(new RefreshTokenCommand());

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    private IActionResult HandleError<T>(Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.NotFound => NotFound(new { error = result.Message }),
            ErrorType.Validation => BadRequest(new { error = result.Message }),
            ErrorType.Conflict => Conflict(new { error = result.Message }),
            ErrorType.Unauthorized => Unauthorized(new { error = result.Message }),
            ErrorType.Internal => StatusCode(500, new { error = result.Message }),
            _ => StatusCode(500, new { error = result.Message ?? "Unhandled error" })
        };
    }
}