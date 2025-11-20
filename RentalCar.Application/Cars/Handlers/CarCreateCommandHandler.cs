using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Common.Constants;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Images.Mappers;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Cars.Handlers
{
    public class CarCreateCommandHandler
        (IValidator<CarCreateCommand> validator,
        IUnitOfWork unitOfWork,
        ICarRepository repository,
        IModelRepository modelRepository,
        IMakeRepository makeRepository,
        IFileService fileService,
        ICarImageRepository imageRepository) : ICommandHandler<CarCreateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(CarCreateCommand command)
        {
            var validations = validator.Validate(command);
            if(!validations.IsValid)
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var model = await modelRepository.GetByIdAsync(command.ModelId);
            if (model == null)
                return Result<string>.Fail("Model not found.", ErrorType.NotFound);

            var make = await makeRepository.GetByIdAsync(model.MakeId);

            var car = command.ToEntity(model.Name, make!.Name);
            await repository.CreateAsync(car);
            await unitOfWork.SaveChangesAsync();

            foreach (var picture in command.Pictures)
            {
                var fileName = await fileService.SaveFileAsync(UploadFolders.Cars, picture);
                var image = ImageMappers.ToEntity(fileName, car.Id);
                await imageRepository.CreateAsync(image);
            }

            await unitOfWork.SaveChangesAsync();
            return Result<string>.Ok(null,"Car created successfully");

        }
    }
}
