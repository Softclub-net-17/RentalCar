using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/cars")]
[ApiController]
[Authorize]
public class CarController(
    IQueryHandler<CarGetByIdQuery, Result<CarGetDto>> getByIdHandler,
    IQueryHandler<CarGetQuery, Result<List<CarGetDto>>> getall)
    : ControllerBase
{ 
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    { 
        var result = await getall.HandleAsync(new CarGetQuery()); 
        if (!result.IsSuccess) 
            return HandleError(result);

        return Ok(result.Data);
    }
        
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    { 
        var result = await getByIdHandler.HandleAsync(new CarGetByIdQuery(id));
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