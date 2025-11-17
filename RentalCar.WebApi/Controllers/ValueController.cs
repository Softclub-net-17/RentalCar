using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.Commands;
using RentalCar.Application.Values.DTOS;
using RentalCar.Application.Values.Queries;

namespace RentalCar.WebApi.Controllers;

[ApiController]
[Route("api/values")]
[Authorize(Roles = "Admin")]
public class ValueController(
    IQueryHandler<ValueGetQuery, Result<List<ValueGetDto>>> getAllHandler,
    IQueryHandler<ValueGetByIdQuery, Result<ValueGetDto>> getByIdHandler,
    ICommandHandler<ValueCreateCommand, Result<string>> createHandler,
    ICommandHandler<ValueUpdateCommand, Result<string>> updateHandler,
    ICommandHandler<ValueDeleteCommand, Result<string>> deleteHandler)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await getAllHandler.HandleAsync(new ValueGetQuery());
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await getByIdHandler.HandleAsync(new ValueGetByIdQuery(id));
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(ValueCreateCommand command)
    {
        var result = await createHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Created("", new { message = result.Message });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ValueUpdateCommand command)
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
        var result = await deleteHandler.HandleAsync(new ValueDeleteCommand(id));
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