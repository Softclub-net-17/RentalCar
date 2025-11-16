using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Mappers;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarAttributes.Handlers;

public class CarAttributeGetQueryHandler(
    ICarAttributeRepository carAttributeRepository
    ) : IQueryHandler<CarAttributeGetQuery, Result<List<CarAttributeGetDto>>>
{
    public async Task<Result<List<CarAttributeGetDto>>> HandleAsync(CarAttributeGetQuery command)
    {
        var carAttributes = await carAttributeRepository.GetAllAsync();
        var item = carAttributes.ToDto();
        
        return Result<List<CarAttributeGetDto>>.Ok(item);
    }
}
