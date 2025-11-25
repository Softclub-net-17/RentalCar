using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.DTOs;
using RentalCar.Application.Models.Mappers;
using RentalCar.Application.Models.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Models.Handlers
{
    public class ModelsGetByMakeQueryHandler(IModelRepository modelRepository) 
        : IQueryHandler<ModelsGetByMakeQuery, Result<List<ModelGetDto>>>
    {
        public async Task<Result<List<ModelGetDto>>> HandleAsync(ModelsGetByMakeQuery query)
        {
            var models = await modelRepository.GetModelsByMakeWithCarsAsync(query.MakeId);

            var items = models.ToDto();

            return Result<List<ModelGetDto>>.Ok(items);
        }
    }
}