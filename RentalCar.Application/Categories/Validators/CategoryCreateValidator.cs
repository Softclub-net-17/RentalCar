using RentalCar.Application.Categories.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Categories.Validators
{
    public class CategoryCreateValidator : IValidator<CategoryCreateCommand>
    {
        public ValidationResult Validate(CategoryCreateCommand instance)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.Name))
                result.AddError("Name is required");

            if (instance.Name.Length < 2)
                result.AddError("Name is too short");

            if (instance.Name.Length > 25)
                result.AddError("Name is too long");

            return result;
            
        }
    }
}
