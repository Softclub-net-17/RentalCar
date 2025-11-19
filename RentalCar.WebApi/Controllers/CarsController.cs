using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController
               (ICommandHandler<CarCreateCommand, Result<string>> create,
         ICommandHandler<CarUpdateCommand, Result<string>> update,
         ICommandHandler<CarDeleteCommand, Result<string>> delete,
         IQueryHandler<CarsGetQuery, Result<List<CarsGetDto>>> getall)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await getall.HandleAsync(new CarsGetQuery());
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
}
