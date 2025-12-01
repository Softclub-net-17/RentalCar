using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/auth")]
[ApiController]
public class AuthController(
    ICommandHandler<LoginCommand, Result<string>> loginCommandHandler,
    ICommandHandler<RegisterCommand, Result<string>> registerCommandHandler,
    ICommandHandler<ChangePasswordCommand, Result<string>> changePasswordHandler,
    ICommandHandler<RequestResetPasswordCommand, Result<string>> requestReset,
    ICommandHandler<VerifyCodeCommand, Result<string>> verify,
    ICommandHandler<ResetPasswordCommand, Result<string>> reset,
    ICommandHandler<ChangeEmailCommand,Result<string>> changeEmail,
    ICommandHandler<RequestChangeEmailCommand,Result<string>> requestEmail) 
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
    [HttpPost("request-reset-password")]
    public async Task<IActionResult> RequestResetPasswordAsync(RequestResetPasswordCommand command)
    {
        var result = await requestReset.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Message);
    }
    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyCodeAsync(VerifyCodeCommand command)
    {
        var result = await verify.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Message);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordCommand command)
    {
        var result = await reset.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Message);
    }
    [Authorize]
    [HttpPost("request-change-email")]
    public async Task<IActionResult> RequestChangeEmailAsync([FromBody] RequestChangeEmailDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
        var userId = int.Parse(userIdClaim.Value);

        var command = new RequestChangeEmailCommand
        {
            UserId = userId,
            NewEmail = dto.NewEmail,
            Password = dto.Password
        };

        var result = await requestEmail.HandleAsync(command);
        return result.IsSuccess ? Ok(result.Message) : HandleError(result);
    }
    [Authorize]
    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
            return Unauthorized("UserId claim not found.");

        var userId = int.Parse(userIdClaim.Value);

        var command = new ChangeEmailCommand
        {
            UserId = userId,
            Code = dto.Code
        };

        var result = await changeEmail.HandleAsync(command);
        return result.IsSuccess ? Ok(result.Message) : HandleError(result);
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