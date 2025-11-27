using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork,
    IValidator<ChangePasswordCommand> validator)
    : ICommandHandler<ChangePasswordCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ChangePasswordCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }

        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Result<string>.Fail("Unauthorized", ErrorType.Unauthorized);

        var userId = int.Parse(userIdClaim.Value);

        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<string>.Fail("User not found", ErrorType.NotFound);

        var isCorrect = PasswordHasher.Verify(command.OldPassword, user.PasswordHash);
        if (!isCorrect)
            return Result<string>.Fail("Old password is incorrect", ErrorType.Validation);

        user.PasswordHash = PasswordHasher.HashPassword(command.NewPassword);

        await userRepository.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "Password changed successfully");
    }
}
