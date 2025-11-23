using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.Commands;
using RentalCar.Application.Users.DTOS;
using RentalCar.Application.Users.Queries;

namespace RentalCar.WebApi.Controllers.Admin;

[ApiExplorerSettings(GroupName = "admin")]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
[ApiController]
public class UserController(
    IQueryHandler<UserGetQuery, Result<List<UserGetDto>>> getAllHandler,
    IQueryHandler<UserGetByIdQuery, Result<UserGetDto>> getByIdHandler,
    ICommandHandler<UserUpdateCommand, Result<string>> updateHandler,
    ICommandHandler<UserDeleteCommand, Result<string>> deleteHandler)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await getAllHandler.HandleAsync(new UserGetQuery());
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await getByIdHandler.HandleAsync(new UserGetByIdQuery(id));
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserUpdateCommand command)
    {
        command.Id = id;
        var result = await updateHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(new { message = result.Message });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await deleteHandler.HandleAsync(new UserDeleteCommand(id));
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(new { message = result.Message });
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