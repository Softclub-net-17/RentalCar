using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Statistics.DTOs;
using RentalCar.Application.Statistics.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.WebApi.Controllers.Admin
{
    [Route("api/admin/statistics")]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class StatisticsController(IQueryHandler<GetRentalStatisticsQuery,Result<StatisticsDto>> getStatistics) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> CountAsync()
        {
            var result = await getStatistics.HandleAsync(new GetRentalStatisticsQuery());
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
}
