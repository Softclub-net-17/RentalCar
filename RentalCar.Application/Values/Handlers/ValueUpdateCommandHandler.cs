using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Values.Commands;
using RentalCar.Application.Values.Mappers;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Values.Handlers;

public class ValueUpdateCommandHandler(IValueRepository valueRepository,
    IUnitOfWork unitOfWork,
    IValidator<ValueUpdateCommand> validator) 
    : ICommandHandler<ValueUpdateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ValueUpdateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result<string>.Fail(string.Join(";", validationResult.Errors.Select(s => s)), ErrorType.Validation);
        }
        
        var student = await valueRepository.GetByIdAsync(command.Id);
        if (student == null)
        {
            return Result<string>.Fail("Value not found.", ErrorType.NotFound);
        }
        
        command.MapFrom(student);

        await valueRepository.UpdateAsync(student);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "Value updated successfully.");
    }
}