using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Makes.Commands;
using RentalCar.Application.Makes.DTOs;
using RentalCar.Application.Makes.Queries;
using RentalCar.Application.Models.DTOs;
using RentalCar.Application.Models.Queries;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/makes")]
[ApiController]
[Authorize]
public class MakeController(
         IQueryHandler<MakeGetQuery, Result<List<MakeGetDto>>> getall,
         IQueryHandler<ModelGetByMakeIdQuery, Result<List<ModelGetDto>>> getAllModel)
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