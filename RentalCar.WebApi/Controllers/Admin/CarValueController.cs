using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.CarValues.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Admin;

[Route("api/admin/car-values")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Roles = "Admin")]
[ApiController]
public class CarValueController(
    ICommandHandler<CarValueCreateCommand, Result<string>> createHandler,
    ICommandHandler<CarValueUpdateCommand, Result<string>> updateHandler,
    ICommandHandler<CarValueDeleteCommand, Result<string>> deleteHandler)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CarValueCreateCommand command)
    {
        var result = await createHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Created("", new { message = result.Message });
    }

    [HttpPut("{carId:int}/{valueId:int}")]
    public async Task<IActionResult> UpdateAsync(int carId,int valueId, [FromBody] CarValueUpdateCommand command)
    {
        command.CarId = carId;
        command.ValueId = valueId;
        var result = await updateHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(new { message = result.Message });
    }

    [HttpDelete("{carId:int}/{valueId:int}")]
    public async Task<IActionResult> DeleteAsync(int carId,int valueId)
    {
        var result = await deleteHandler.HandleAsync(new CarValueDeleteCommand(carId, valueId));
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