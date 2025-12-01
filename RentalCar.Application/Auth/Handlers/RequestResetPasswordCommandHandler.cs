using Microsoft.AspNetCore.Http;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Helpers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.VerificationCodes.Mappers;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Auth.Handlers
{
        public class RequestResetPasswordCommandHandler
            (IUnitOfWork unitOfWork,
            IVerificationCodeRepository verificationCodeRepository,
            IEmailService emailService,
            IValidator<RequestResetPasswordCommand> validator,
            IUserRepository userRepository)
            : ICommandHandler<RequestResetPasswordCommand, Result<string>>
        {
            public async Task<Result<string>> HandleAsync(RequestResetPasswordCommand command)
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

            var code = VerificationCodeGenerator.GenerateCode();

            var codeHash = CodeHasher.HashCode(code);

            var existing = await verificationCodeRepository.GetByEmailAsync(command.Email);

            if (existing != null)
            {
                existing.CodeHash = codeHash;
                existing.Expiration = DateTime.UtcNow.AddMinutes(2);
                existing.IsUsed = false;

                await verificationCodeRepository.UpdateAsync(existing);
            }
            else
            {
                var verificationCode = VerificationCodeMappers.ToEntity(codeHash,command.Email);
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
