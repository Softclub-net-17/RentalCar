using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Validators
{
    public class ModelUpdateValidator : IValidator<ModelUpdateCommand>
    {
        public ValidationResult Validate(ModelUpdateCommand instance)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.Name))
                result.AddError("Name is required");

            if (instance.Name.Length > 45)
                result.AddError("Name is too long");

            return result;
        }
    }
}
