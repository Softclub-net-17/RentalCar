using RentalCar.Application.Categories.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.DTOs;
using RentalCar.Application.Models.Mappers;
using RentalCar.Application.Models.Queries;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Handlers
{
    public class ModelGetQueryHandler(IModelRepository modelRepository) : IQueryHandler<ModelsGetQuery, Result<List<ModelGetDto>>>
    {
        public async Task<Result<List<ModelGetDto>>> HandleAsync(ModelsGetQuery query)
        {
            var models = await modelRepository.GetAllAsync();
            var items = models.ToDto();

            return Result<List<ModelGetDto>>.Ok(items);
        }
    }
}
