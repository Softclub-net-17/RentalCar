using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Cars.Handlers
{
    public class CarDeleteCommandHandler
        (IUnitOfWork unitOfWork,
        ICarRepository carRepository) : ICommandHandler<CarDeleteCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(CarDeleteCommand command)
        {
            var car = await carRepository.GetByIdAsync(command.Id);
            if (car == null)
            {
                return Result<string>.Fail("Car not found.", ErrorType.NotFound);
            }

            await carRepository.DeleteAsync(car);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Car deleted successfully.");
        }
    }
}
