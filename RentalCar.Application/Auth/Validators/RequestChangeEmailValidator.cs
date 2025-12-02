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
    public class RequestChangeEmailValidator : IValidator<RequestChangeEmailCommand>
    {
        public ValidationResult Validate(RequestChangeEmailCommand instance)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.NewEmail))
            {
                validationResult.AddError("Email is required.");
            }
            
            if(!instance.NewEmail.Contains("@"))
            {
                validationResult.AddError("Email format is invalid.");  
            }

            return validationResult;    

        }
    }
}
