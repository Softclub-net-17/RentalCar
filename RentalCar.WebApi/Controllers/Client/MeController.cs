using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.DTOS;
using RentalCar.Application.Users.Queries;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/makes")]
[ApiController]
public class MeController(
    IQueryHandler<UserGetMeQuery, Result<UserGetDto>> getmehandler,
    IQueryHandler<UserGetMeReservationQuery, Result<UserReservationGetDto>> resermehandler)
    : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var result = await getmehandler.HandleAsync(new UserGetMeQuery());
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
    
    [HttpGet("me-reservation")]
    public async Task<IActionResult> GetReservMe()
    {
        var result = await resermehandler.HandleAsync(new UserGetMeReservationQuery());
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
    
    private IActionResult HandleError<T>(Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.Validation => BadRequest(new { error = result.Message }),
            ErrorType.NotFound => NotFound(new { error = result.Message }),
            ErrorType.Conflict => Conflict(new { error = result.Message }),
            _ => StatusCode(500, new { error = result.Message ?? "Internal server error" })
        };
    }
}