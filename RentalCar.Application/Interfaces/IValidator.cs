using System.ComponentModel.DataAnnotations;
using ValidationResult = RentalCar.Application.Common.Validations.ValidationResult;

namespace RentalCar.Application.Interfaces;

public interface IValidator<T>
{
    ValidationResult Validate(T instance);
}