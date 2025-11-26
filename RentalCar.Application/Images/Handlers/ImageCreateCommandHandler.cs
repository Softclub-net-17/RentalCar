using RentalCar.Application.Common.Constants;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Images.Commands;
using RentalCar.Application.Images.Mappers;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Images.Handlers
{
    public class ImageCreateCommandHandler(
      IFileService fileService,
      IImageRepository repository,
      IUnitOfWork unitOfWork,
      ICarRepository carRepository,
      IMakeRepository makeRepository)
      : ICommandHandler<ImageCreateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(ImageCreateCommand command)
        {
            if (command.CarId == null && command.MakeId == null)
                return Result<string>.Fail("Either CarId or MakeId must be provided", ErrorType.Validation);


            if (command.CarId != null)
            {
                var car = await carRepository.GetByIdAsync(command.CarId.Value);
                if (car == null)
                    return Result<string>.Fail("Car not found", ErrorType.NotFound);

                foreach (var file in command.Files)
                {
                    var fileName = await fileService.SaveFileAsync(UploadFolders.Cars, file);
                    var image = ImageMappers.ToCarImage(fileName, car.Id);
                    await repository.CreateAsync(image);
                }
            }
            if (command.MakeId != null)
            {
                var make = await makeRepository.GetByIdAsync(command.MakeId.Value);
                if (make == null)
                    return Result<string>.Fail("Make not found", ErrorType.NotFound);

                foreach (var file in command.Files)
                {
                    var fileName = await fileService.SaveFileAsync(UploadFolders.Makes, file);
                    var image = ImageMappers.ToMakeImage(fileName, make.Id);
                    await repository.CreateAsync(image);
                    make.Image = image;
                }
            }

            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null,"Image created");
        }
    }

}
