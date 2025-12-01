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
    public class ChangeEmailCommandHandler
        (IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IVerificationCodeRepository verificationCodeRepository,
        IEmailService emailService) : ICommandHandler<ChangeEmailCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(ChangeEmailCommand command)
        {
            var user = await userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                return Result<string>.Fail("User not found.", ErrorType.NotFound);

            var verification = await verificationCodeRepository.GetActiveByUserAsync(command.UserId);
            if (verification == null)
                return Result<string>.Fail("No active verification code.", ErrorType.NotFound);

            if (verification.Expiration < DateTime.UtcNow)
                return Result<string>.Fail("Verification code expired.", ErrorType.Conflict);

            if (verification.IsUsed)
                return Result<string>.Fail("Verification code already used.", ErrorType.Conflict);

            if (!CodeHasher.VerifyCode(verification.CodeHash, command.Code))
                return Result<string>.Fail("Invalid verification code.", ErrorType.Unauthorized);

            var oldEmail = user.Email;
            user.Email = verification.NewEmail;
            verification.IsUsed = true;

            await userRepository.UpdateAsync(user);
            await verificationCodeRepository.UpdateAsync(verification);
            await unitOfWork.SaveChangesAsync();

            await emailService.SendEmailAsync(
                new List<string> { verification.NewEmail },
                "Email changed",
                "Your account email has been successfully changed."
            );

            await emailService.SendEmailAsync(
                new List<string> { oldEmail },
                "Email changed",
                $"Your account email was changed to {verification.NewEmail}. If this wasn't you, contact support."
            );

            return Result<string>.Ok(null, "Email successfully changed.");
        }
    }
    }
