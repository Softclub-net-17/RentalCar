using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class RegisterCommandHandler(
    IUserRepository  userRepository
    ,IUnitOfWork unitOfWork
    ,IValidator<RegisterCommand> validator
    ) :  ICommandHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(RegisterCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(s => s));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        if (await userRepository.ExistsByEmailAsync(command.Email))
        {
            return Result<string>.Fail("Email is already registered.", ErrorType.Conflict);
        }
        
        var passwordHash = PasswordHasher.HashPassword(command.Password);
        var user = command.ToEntity(passwordHash);
        
        await userRepository.CreateAsync(user);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "User registered successfully.");
    }
}