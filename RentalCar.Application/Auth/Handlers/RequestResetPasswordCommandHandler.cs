using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Helpers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.VerificationCodes.Mappers;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Auth.Handlers
{
    public class RequestResetPasswordCommandHandler
        (IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IValidator<RequestResetPasswordCommand> validator,
        IVerificationCodeRepository verificationCodeRepository,
        IEmailService emailService) : ICommandHandler<RequestResetPasswordCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(RequestResetPasswordCommand command)
        {
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(s => s));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }
            var code = VerificationCodeGenerator.GenerateCode();

            var existing = await verificationCodeRepository.GetByEmailAsync(command.Email);

            if (existing != null)
            {
                existing.Code = code;
                existing.Expiration = DateTime.UtcNow.AddMinutes(2);
                existing.IsUsed = false;

                await verificationCodeRepository.UpdateAsync(existing);
            }
            else
            {
                var verificationCode = VerificationCodeMappers.ToEntity(code, command.Email);
                await verificationCodeRepository.CreateAsync(verificationCode);
            }

            await unitOfWork.SaveChangesAsync();

            await emailService.SendEmailAsync(
                new List<string> { command.Email },
                "Password Reset Verification Code",
                $"Your password reset verification code is: {code}. It will expire in 2 minutes."
            );

            return Result<string>.Ok(null, "Verification code sent to email.");
        }
    }
}
