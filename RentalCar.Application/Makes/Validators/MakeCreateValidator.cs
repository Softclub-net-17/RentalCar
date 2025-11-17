using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Makes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Makes.Validators
{
    public class MakeCreateValidator : IValidator<MakeCreateCommand>
    {
        public ValidationResult Validate(MakeCreateCommand instance)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(instance.Name))
                result.AddError("Name is required");

            if (instance.Name.Length > 40)
                result.AddError("Name is too long");

            return result;
        }
    }
}
