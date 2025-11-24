using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Images.Commands;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Admin;

    [Route("api/admin/images")]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ImagesController
           (ICommandHandler<ImageCreateCommand, Result<string>> create,
         ICommandHandler<ImageDeleteCommand, Result<string>> delete)
        : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ImageCreateCommand command)
        {
            var result = await create.HandleAsync(command);
            if (!result.IsSuccess)
                return HandleError(result);

            return Created("", new { message = result.Message });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await delete.HandleAsync(new ImageDeleteCommand(id));
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
