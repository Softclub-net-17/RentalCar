using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.Commands;
using RentalCar.Application.Models.DTOs;
using RentalCar.Application.Models.Queries;

namespace RentalCar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController
               (ICommandHandler<ModelCreateCommand, Result<string>> create,
         ICommandHandler<ModelUpdateCommand, Result<string>> update,
         ICommandHandler<ModelDeleteCommand, Result<string>> delete,
         IQueryHandler<ModelsGetQuery, Result<List<ModelGetDto>>> getall)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await getall.HandleAsync(new ModelsGetQuery());
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ModelCreateCommand command)
        {
            var result = await create.HandleAsync(command);
            if (!result.IsSuccess)
                return HandleError(result);

            return Created("", new { message = result.Message });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ModelUpdateCommand command)
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
            var result = await delete.HandleAsync(new ModelDeleteCommand(id));
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
