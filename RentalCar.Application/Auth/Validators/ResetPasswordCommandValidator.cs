using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Auth.Validators
{
    public class ResetPasswordCommandValidator : IValidator<ResetPasswordCommand>
    {
        public ValidationResult Validate(ResetPasswordCommand instance)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.NewPassword))
                validationResult.AddError("New password is required.");

            if (instance.NewPassword != instance.ConfirmPassword)
                validationResult.AddError("New password and confirm password do not match.");

            return validationResult;
        }
    }
}
