using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Makes.Commands;
using RentalCar.Application.Makes.DTOs;
using RentalCar.Application.Makes.Queries;
using RentalCar.Application.Models.DTOs;
using RentalCar.Application.Models.Queries;

namespace RentalCar.WebApi.Controllers
{
    [Route("api/makes")]
    [ApiController]
    public class MakesController
             (ICommandHandler<MakeCreateCommand, Result<string>> create,
         ICommandHandler<MakeUpdateCommand, Result<string>> update,
         ICommandHandler<MakeActivateCommand, Result<string>> activate,
         ICommandHandler<MakeDeactivateCommand, Result<string>> deactivate,
         IQueryHandler<MakeGetQuery, Result<List<MakeGetDto>>> getall,
         IQueryHandler<ModelGetByMakeIdQuery,Result<List<ModelGetDto>>> getAllModel)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await getall.HandleAsync(new MakeGetQuery());
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(result.Data);
        }
        [HttpGet("{id:int}/models")]
        public async Task<IActionResult> GetModelsByMakeId(int id)
        {
            var result = await getAllModel.HandleAsync(new ModelGetByMakeIdQuery(id));
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MakeCreateCommand command)
        {
            var result = await create.HandleAsync(command);
            if (!result.IsSuccess)
                return HandleError(result);

            return Created("", new { message = result.Message });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] MakeUpdateCommand command)
        {
            command.Id = id;
            var result = await update.HandleAsync(command);
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(new { message = result.Message });
        }

        [HttpPut("activate/{id:int}")]
        public async Task<IActionResult> ActivateAsync(int id)
        {
            var result = await activate.HandleAsync(new MakeActivateCommand(id));
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(new { message = result.Message });
        }

        [HttpPut("deactivate/{id:int}")]
        public async Task<IActionResult> DeactivateAsync(int id)
        {
            var result = await deactivate.HandleAsync(new MakeDeactivateCommand(id));
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
