using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Auth.Validators
{
    public class RequestResetPasswordValidator : IValidator<RequestResetPasswordCommand>
    {
        public ValidationResult Validate(RequestResetPasswordCommand instance)
        {
            var result= new ValidationResult();

            if(string.IsNullOrWhiteSpace(instance.Email))
            {
                result.AddError("Email is required.");
            }

            if(!instance.Email.Contains("@"))
            {
                result.AddError("Invalid email format.");
            }

            return result;
        }
    }
}
