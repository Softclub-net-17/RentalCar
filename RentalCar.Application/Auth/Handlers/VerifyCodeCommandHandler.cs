
using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Helpers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RentalCar.Application.Auth.Handlers;

public class VerifyCodeCommandHandler(
    IVerificationCodeRepository verificationCodeRepository,
    IValidator<VerifyCodeCommand> validator,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository) 
    : ICommandHandler<VerifyCodeCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(VerifyCodeCommand command)
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

        var verificationCode = await verificationCodeRepository.GetByEmailAsync(command.Email);
        if (verificationCode == null)
        {
            return Result<string>.Fail("No verification code found.", ErrorType.NotFound);
        }

        if (verificationCode.Expiration < DateTime.UtcNow)
        {
            return Result<string>.Fail("Verification code has expired.", ErrorType.Validation);
        }

        if (verificationCode.IsUsed)
        {
            return Result<string>.Fail("Verification code has already been used.", ErrorType.Validation);
        }

        
        if (!CodeHasher.VerifyCode(verificationCode.CodeHash, command.Code))
        {
            return Result<string>.Fail("Invalid verification code.", ErrorType.Unauthorized);
        }

        
        verificationCode.IsUsed = true;
        await verificationCodeRepository.UpdateAsync(verificationCode);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "Verification successful.");
    }
}
