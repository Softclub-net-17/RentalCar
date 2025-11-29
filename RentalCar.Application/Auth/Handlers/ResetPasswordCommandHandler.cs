using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.Mappers;
using RentalCar.Application.VerificationCodes.Mappers;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IVerificationCodeRepository verificationCodeRepository,
    IValidator<ResetPasswordCommand> validator,
    IUnitOfWork unitOfWork) 
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
        {
            return Result<string>.Fail("User not found.", ErrorType.NotFound);
        }
        
        var passwordHash = PasswordHasher.HashPassword(command.NewPassword);
        user.MapFrom(passwordHash);
        
        var lastCode = await verificationCodeRepository.GetByEmailAsync(user.Email);
        lastCode.DeActivate();

        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "Password has been reset successfully.");
    }
}