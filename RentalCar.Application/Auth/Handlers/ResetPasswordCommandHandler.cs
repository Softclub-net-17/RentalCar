using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.Mappers;
using RentalCar.Application.VerificationCodes.Mappers;
using RentalCar.Domain.Interfaces;
using System.Security.Claims;

namespace RentalCar.Application.Auth.Handlers;

public class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IValidator<ResetPasswordCommand> validator,
    IUnitOfWork unitOfWork,
    IVerificationCodeRepository verificationCodeRepository)
    : ICommandHandler<ResetPasswordCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ResetPasswordCommand command)
    {

        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(s => s));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }

        var user = await userRepository.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<string>.Fail("User not found.", ErrorType.NotFound);

        
        var verification = await verificationCodeRepository.GetByEmailAsync(command.Email);
        if (verification == null || !verification.IsUsed)
            return Result<string>.Fail("Email is not verified for password reset.", ErrorType.Unauthorized);

        if (verification.Expiration < DateTime.UtcNow)
            return Result<string>.Fail("Verification code has expired. Please request a new one.", ErrorType.Conflict);

        
        var passwordHash = PasswordHasher.HashPassword(command.NewPassword);
        user.PasswordHash = passwordHash;
        await userRepository.UpdateAsync(user);

        
        verification.DeActivate();
        await verificationCodeRepository.UpdateAsync(verification);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "Password reset successfully.");
    }
    }