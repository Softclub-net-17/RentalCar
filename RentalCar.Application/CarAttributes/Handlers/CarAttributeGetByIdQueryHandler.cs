using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.CarAttributes.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarAttributes.Handlers;

public class CarAttributeGetByIdQueryHandler(
    ICarAttributeRepository carAttributeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CarAttributeGetByIdQuery, Result<CarAttributeGetDto>>
{
    public async Task<Result<CarAttributeGetDto>> HandleAsync(CarAttributeGetByIdQuery command)
    {
        var carAttribute = await carAttributeRepository.GetByIdAsync(command.Id);

        if (carAttribute == null)
            return Result<CarAttributeGetDto>.Fail("CarAttribute attribute not found.", ErrorType.NotFound);

        var car = CarAttributeMappers.ToDto(carAttribute);

        return Result<CarAttributeGetDto>.Ok(car);
    }
}