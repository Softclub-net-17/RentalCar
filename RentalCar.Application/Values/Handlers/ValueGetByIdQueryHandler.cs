using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.DTOS;
using RentalCar.Application.Values.Mappers;
using RentalCar.Application.Values.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Values.Handlers;

public class ValueGetByIdQueryHandler(
    IValueRepository valueRepository,
    IUnitOfWork unitOfWork)
    : IQueryHandler<ValueGetByIdQuery, Result<ValueGetDto>>
{
    public async Task<Result<ValueGetDto>> HandleAsync(ValueGetByIdQuery command)
    {
        var carAttribute = await valueRepository.GetByIdAsync(command.Id);

        if (carAttribute == null)
            return Result<ValueGetDto>.Fail("CarAttribute attribute not found.", ErrorType.NotFound);

        var car = ValueMappers.ToDto(carAttribute);

        return Result<ValueGetDto>.Ok(car);
    }
}