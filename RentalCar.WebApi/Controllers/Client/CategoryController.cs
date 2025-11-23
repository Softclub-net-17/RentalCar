using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Categories.DTOs;
using RentalCar.Application.Categories.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.WebApi.Controllers.Client;

[ApiExplorerSettings(GroupName = "client")]
[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoryController (
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