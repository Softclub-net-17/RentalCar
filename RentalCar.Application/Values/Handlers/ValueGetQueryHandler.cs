using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.DTOS;
using RentalCar.Application.Values.Mappers;
using RentalCar.Application.Values.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Values.Handlers;

public class ValueGetQueryHandler(IValueRepository valueRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ValueGetQuery, Result<List<ValueGetDto>>>
{
    public async Task<Result<List<ValueGetDto>>> HandleAsync(ValueGetQuery command)
    {
        var value = await valueRepository.GetAllAsync();
        var items = ValueMappers.ToDto(value);
        
        return Result<List<ValueGetDto>>.Ok(items);
    }
}