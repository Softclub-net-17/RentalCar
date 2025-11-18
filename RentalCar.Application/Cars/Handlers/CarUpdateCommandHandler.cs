using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Cars.Handlers
{
    public class CarUpdateCommandHandler
        (IUnitOfWork unitOfWork,
        ICarRepository carRepository,
        IValidator<CarUpdateCommand> validator) : ICommandHandler<CarUpdateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(CarUpdateCommand command)
        {
            var validations = validator.Validate(command);
            if(!validations.IsValid)
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var car = await carRepository.GetByIdAsync(command.Id);

            if (car == null)
            {
                return Result<string>.Fail("Car not found", ErrorType.NotFound);
            }

            command.MapFrom(car);

            await carRepository.UpdateAsync(car);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Car updated successfully");

        }
    }
}
