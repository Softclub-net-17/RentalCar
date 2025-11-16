using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.Commands;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Values.Handlers;

public class ValueDeleteCommandHandler(
    IValueRepository valueRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<ValueDeleteCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ValueDeleteCommand command)
    {
        var value = await valueRepository.GetByIdAsync(command.Id);
        if (value == null)
        {
            return Result<string>.Fail("Value not found.", ErrorType.NotFound);
        }

        await valueRepository.DeleteAsync(value);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "Value deleted successfully.");
    }
}