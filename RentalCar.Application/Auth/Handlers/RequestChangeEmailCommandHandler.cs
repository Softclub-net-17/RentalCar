using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Helpers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.VerificationCodes.Mappers;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers
{
    public class RequestChangeEmailCommandHandler
        (IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IVerificationCodeRepository verificationCodeRepository,
        IEmailService emailService,
        IValidator<RequestChangeEmailCommand> validator) : ICommandHandler<RequestChangeEmailCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(RequestChangeEmailCommand command)
        {
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var user = await userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                return Result<string>.Fail("User not found.", ErrorType.NotFound);

            var passwordOk = await userRepository.VerifyPasswordAsync(command.UserId, command.Password);
            if (!passwordOk)
                return Result<string>.Fail("Invalid password.", ErrorType.Unauthorized);

            var exists = await userRepository.ExistsByEmailAsync(command.NewEmail);
            if (exists)
                return Result<string>.Fail("New email is already in use.", ErrorType.Conflict);

            var code = VerificationCodeGenerator.GenerateCode();
            var codeHash = CodeHasher.HashCode(code);

            var existing = await verificationCodeRepository.GetByUserAndEmailAsync(command.UserId, command.NewEmail);
            if (existing != null)
            {
                existing.UpdateFrom(codeHash, 5);
                await verificationCodeRepository.UpdateAsync(existing);
            }
            else
            {
                var verificationCode = VerificationCodeMappers.ToEntity(command.NewEmail, codeHash, 5);
                verificationCode.UserId = command.UserId;
                await verificationCodeRepository.CreateAsync(verificationCode);
            }

            await unitOfWork.SaveChangesAsync();

            await emailService.SendEmailAsync(
                new List<string> { command.NewEmail },
                "Confirm your new email",
                $"Your verification code is: {code}"
            );

            return Result<string>.Ok(null, $"Verification code sent to {command.NewEmail}.");
        }
    }
}
