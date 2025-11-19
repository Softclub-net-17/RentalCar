using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Images.Mappers;
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
        IValidator<CarUpdateCommand> validator,
        ICarImageRepository carImageRepository,
        IFileService fileService) : ICommandHandler<CarUpdateCommand, Result<string>>
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

            var oldImages = await carImageRepository.GetByCarId(car.Id);

            foreach(var image in oldImages)
            {
                await fileService.DeleteFileAsync("cars", image.PhotoUrl);
                await carImageRepository.DeleteAsync(image);
            }

            foreach(var picture in command.Pictures)
            {
                var fileName = await fileService.SaveFileAsync("cars", picture);
                var newImage = ImageMappers.ToEntity(fileName, car.Id);
                await carImageRepository.CreateAsync(newImage);
            }

            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Car updated successfully");

        }
    }
}
