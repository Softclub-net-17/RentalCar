using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Favorites.Commands;
using RentalCar.Application.Favorites.DTOs;
using RentalCar.Application.Favorites.Queries;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Client
{
    [ApiExplorerSettings(GroupName = "client")]
    [Route("api/favorites")]
    [ApiController]
    [Authorize]
    public class FavoritesController(ICommandHandler<FavoriteCreateCommand, Result<string>> create,
        ICommandHandler<FavoriteDeleteCommand, Result<string>> delete,
        IQueryHandler<FavoritesGetQuery, Result<List<FavoriteGetDto>>> getAll) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await getAll.HandleAsync(new FavoritesGetQuery());
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(result.Data);
        }
        [HttpDelete("{carId}")]
        public async Task<IActionResult> DeleteAsync(int carId)
        {
            var result = await delete.HandleAsync(new FavoriteDeleteCommand(carId));
            if (!result.IsSuccess)
                return HandleError(result);

            return Ok(new { message = result.Message });
        }
        [HttpPost("{carId}")]
        public async Task<IActionResult> CreateAsync(int carId)
        {
            var result = await create.HandleAsync(new FavoriteCreateCommand(carId));
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
}
