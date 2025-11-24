using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Reservations.Commands;
using RentalCar.Application.Reservations.DTOS;
using RentalCar.Application.Reservations.Queries;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/reservations")]
[ApiController]
[Authorize]
public class ReservationController(
    IQueryHandler<ReservationGetQuery, Result<List<ReservationGetDto>>> getAllHandler,
    IQueryHandler<ReservationGetByIdQuery, Result<ReservationGetDto>> getByIdHandler,
    ICommandHandler<ReservationCreateCommand, Result<string>> createHandler)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await getAllHandler.HandleAsync(new ReservationGetQuery());
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await getByIdHandler.HandleAsync(new ReservationGetByIdQuery(id));
        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(ReservationCreateCommand command)
    {
        var result = await createHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return HandleError(result);

        return Created("", new { message = result.Message });
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