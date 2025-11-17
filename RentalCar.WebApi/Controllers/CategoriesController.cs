using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.Categories.Commands;
using RentalCar.Application.Categories.DTOs;
using RentalCar.Application.Categories.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController
        (ICommandHandler<CategoryCreateCommand, Result<string>> create,
         ICommandHandler<CategoryUpdateCommand, Result<string>> update,
         ICommandHandler<CategoryActivateCommand, Result<string>> activate,
         ICommandHandler<CategoryDeactivateCommand, Result<string>> deactivate,
         IQueryHandler<CategoriesGetQuery, Result<List<CategoryGetDto>>> getall)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await getall.HandleAsync(new CategoriesGetQuery());
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryCreateCommand command)
        {
            var result = await create.HandleAsync(command);
            if (!result.IsSuccess)
                return HandleError(result);

            return Created("", new { message = result.Message });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryUpdateCommand command)
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
            var result = await activate.HandleAsync(new CategoryActivateCommand(id));
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(new { message = result.Message });
        }

        [HttpPut("deactivate/{id:int}")]
        public async Task<IActionResult> DeactivateAsync(int id)
        {
            var result = await deactivate.HandleAsync(new CategoryDeactivateCommand(id));
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