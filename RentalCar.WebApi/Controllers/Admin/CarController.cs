using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.ValueObject;

namespace RentalCar.WebApi.Controllers.Admin;

[Route("api/admin/cars")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Roles = "Admin")]
[ApiController]
public class CarController(
        ICommandHandler<CarCreateCommand, Result<string>> create,
        ICommandHandler<CarUpdateCommand, Result<string>> update,
        ICommandHandler<CarDeleteCommand, Result<string>> delete,
        IQueryHandler<CarGetByIdQuery, Result<CarGetDto>> getByIdHandler,
        IQueryHandler<CarGetQuery, Result<List<CarGetDto>>> getall,
        IQueryHandler<CarByFilterQuery, Result<List<CarListItemDto>>> getByFilter)
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
        
        [HttpPost("filter")]
        public async Task<IActionResult> GetByFilterAsync([FromForm] CarFilter filter)
        {
            var result = await getByFilter.HandleAsync(new CarByFilterQuery(filter));

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

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CarCreateCommand command)
        {
            var result = await create.HandleAsync(command);
            if (!result.IsSuccess)
                return HandleError(result);

            return Created("", new { message = result.Message });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CarUpdateCommand command)
        {
            command.Id = id;
            var result = await update.HandleAsync(command);
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(new { message = result.Message });
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await delete.HandleAsync(new CarDeleteCommand(id));
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
