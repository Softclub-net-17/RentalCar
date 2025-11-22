using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Common.Validations;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Cars.Validators
{
    public class CarUpdateValidator : IValidator<CarUpdateCommand>
    {
        public ValidationResult Validate(CarUpdateCommand instance)
        {
            var result = new ValidationResult();


            if (string.IsNullOrWhiteSpace(instance.Color))
                result.AddError("Color is required");

            if (instance.Color.Length > 20)
                result.AddError("Color name is too long");

            if (instance.Description.Length > 1024)
                result.AddError("Description is too long");
            
            if (instance.PricePerHour <= 0)
                result.AddError("PricePerHour must be greater than 0.");

            if (instance.Year < 1800)
                result.AddError("Year is invalid");

            if (instance.Seats <= 0)
                result.AddError("Seats must be greater than 0.");

            if (instance.ModelId <= 0)
                result.AddError("ModelId is required.");
            
            if (instance.ValueIds.Count == 0)
                result.AddError("At least one value must be selected.");

            return result;
        }
    }
}
