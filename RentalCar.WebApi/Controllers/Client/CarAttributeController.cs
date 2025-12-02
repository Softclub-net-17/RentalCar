using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/car-attribute")]
[ApiController]
[Authorize]
public class CarAttributeController(
    IQueryHandler<CarGetAttributesWithValuesQuery,Result<List<GetCarAttributesWithValuesDto>>> 
        getAttributesWithValues) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCarAttributesWithValues()
    {
        var result = await getAttributesWithValues.HandleAsync(new CarGetAttributesWithValuesQuery());
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