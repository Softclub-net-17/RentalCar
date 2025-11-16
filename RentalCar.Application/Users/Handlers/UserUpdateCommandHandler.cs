using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.Commands;
using RentalCar.Application.Users.Mappers;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Users.Handlers;

public class UserUpdateCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<UserUpdateCommand> validator)
    : ICommandHandler<UserUpdateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UserUpdateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(s => s));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var user = await userRepository.GetByIdAsync(command.Id);
        if (user == null)
            return Result<string>.Fail("User not found.", ErrorType.NotFound);

        if (await userRepository.ExistsByEmailAsync(command.Email) && command.Email != user.Email)
            return Result<string>.Fail("Email already exists.", ErrorType.Conflict);

        var hashedPassword = PasswordHasher.HashPassword(command.Password);

        command.MapToEntity(user, hashedPassword);

        await userRepository.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "User updated successfully.");
    }
}