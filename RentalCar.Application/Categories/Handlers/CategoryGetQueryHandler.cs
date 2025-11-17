using RentalCar.Application.Categories.DTOs;
using RentalCar.Application.Categories.Mappers;
using RentalCar.Application.Categories.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Categories.Handlers
{
    public class CategoryGetQueryHandler(ICategoryRepository categoryRepository)
        : IQueryHandler<CategoriesGetQuery, Result<List<CategoryGetDto>>>
    {
        public async Task<Result<List<CategoryGetDto>>> HandleAsync(CategoriesGetQuery query)
        {
            var categories = await categoryRepository.GetAllAsync();
            var items = categories.ToDto();

            return Result<List<CategoryGetDto>>.Ok(items);
        }
    }
}
