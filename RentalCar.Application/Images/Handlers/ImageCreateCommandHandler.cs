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
      ICarImageRepository repository,
      IUnitOfWork unitOfWork,
      ICarRepository carRepository)
      : ICommandHandler<ImageCreateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(ImageCreateCommand command)
        {
            var carExist = await carRepository.GetByIdAsync(command.CarId);
            if (carExist == null)
                return Result<string>.Fail("Car not found", ErrorType.NotFound);

            // Сохраняем файл
            var fileName = await fileService.SaveFileAsync("cars", command.File);

            // Создаём сущность
            var image = ImageMappers.ToEntity(fileName, command.CarId);

            await repository.CreateAsync(image);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Image created");
        }
    }

}
