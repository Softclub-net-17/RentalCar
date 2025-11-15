using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarAttributeController(
    IQueryHandler<CarAttributeGetQuery, Result<List<CarAttributeGetDto>>> getAllHandler,
    IQueryHandler<CarAttributeGetByIdQuery, Result<CarAttributeGetDto>> getByIdHandler,
    ICommandHandler<CarAttributeCreateCommand, Result<string>> createHandler,
    ICommandHandler<CarAttributeUpdateCommand, Result<string>> updateHandler,
    ICommandHandler<CarAttributeDeleteCommand, Result<string>> deleteHandler)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await getAllHandler.HandleAsync(new CarAttributeGetQuery());
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await getByIdHandler.HandleAsync(new CarAttributeGetByIdQuery(id));
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CarAttributeCreateCommand command)
    {
        var result = await createHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Created("", new { message = result.Message });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CarAttributeUpdateCommand command)
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
        var result = await deleteHandler.HandleAsync(new CarAttributeDeleteCommand(id));
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