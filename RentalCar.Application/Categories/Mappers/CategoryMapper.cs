using RentalCar.Application.Categories.Commands;
using RentalCar.Application.Categories.DTOs;
using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Categories.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToEntity(this CategoryCreateCommand command)
        {
            return new Category
            {
                Name = command.Name,
                IsActive = true,
            };
        }

        public static void MapFrom(this CategoryUpdateCommand command, Category category)
        {
            category.Name = command.Name;
        }

        public static List<CategoryGetDto> ToDto(this IEnumerable<Category> categories)
        {
            return categories.Select(c => new CategoryGetDto()
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
            }).ToList();
        }
    }
}
