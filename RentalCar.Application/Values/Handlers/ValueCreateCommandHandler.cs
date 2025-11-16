using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.Commands;
using RentalCar.Application.Values.Mappers;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Values.Handlers;

public class ValueCreateCommandHandler(
    IValueRepository valueRepository,
    IUnitOfWork unitOfWork,
    IValidator<ValueCreateCommand> validator
    ) : ICommandHandler<ValueCreateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ValueCreateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }

        var value = command.ToEntity();

        await valueRepository.CreateAsync(value);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "Value created successfully.");
    }
}