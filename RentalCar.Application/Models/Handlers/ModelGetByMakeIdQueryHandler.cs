using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.DTOs;
using RentalCar.Application.Models.Mappers;
using RentalCar.Application.Models.Queries;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RentalCar.Application.Models.Handlers
{
    public class ModelGetByMakeIdQueryHandler
        (IModelRepository modelRepository,
        IUnitOfWork unitOfWork) : IQueryHandler<ModelGetByMakeIdQuery, Result<List<ModelGetDto>>>
    {
        public async Task<Result<List<ModelGetDto>>> HandleAsync(ModelGetByMakeIdQuery query)
        {

            var models = await modelRepository.GetByMakeIdAsync(query.MakeId);
            if (!models.Any())
            {
                return Result<List<ModelGetDto>>.Fail("No models found for the specified make ID.", ErrorType.NotFound);

            }

            var items = models.ToDto();

            return Result<List<ModelGetDto>>.Ok(items);
        }
    }
}
