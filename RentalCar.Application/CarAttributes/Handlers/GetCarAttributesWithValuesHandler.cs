using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Mappers;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RentalCar.Application.CarAttributes.Handlers
{
    public class GetCarAttributesWithValuesHandler(ICarAttributeRepository carAttributeRepository) : IQueryHandler<CarGetAttributesWithValuesQuery, Result<List<GetCarAttributesWithValuesDto>>>
    {
        public async Task<Result<List<GetCarAttributesWithValuesDto>>> HandleAsync(CarGetAttributesWithValuesQuery query)
        {
            var carAttribute = await carAttributeRepository.GetCarAttributesWithValuesAsync();

            var item = carAttribute.ToCarAttributeDto();

            return Result<List<GetCarAttributesWithValuesDto>>.Ok(item);
        }
    }
}
