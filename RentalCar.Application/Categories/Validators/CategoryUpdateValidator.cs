using RentalCar.Application.Categories.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Categories.Validators
{
    public class CategoryUpdateValidator : IValidator<CategoryUpdateCommand>
    {
        public ValidationResult Validate(CategoryUpdateCommand instance)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.Name))
                result.AddError("Name is too long");

            if (instance.Name.Length < 2)
                result.AddError("Name is too short");

            if (instance.Name.Length > 25)
                result.AddError("Name is too long");

            return result;
        }
    }
}
