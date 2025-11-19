using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Common.Constants;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Cars.Handlers
{
    public class CarDeleteCommandHandler
        (IUnitOfWork unitOfWork,
        ICarRepository carRepository,
        ICarImageRepository imageRepository,
        IFileService fileService) : ICommandHandler<CarDeleteCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(CarDeleteCommand command)
        {
            var car = await carRepository.GetByIdAsync(command.Id);
            if (car == null)
            {
                return Result<string>.Fail("Car not found.", ErrorType.NotFound);
            }

            var images = await imageRepository.GetByCarId(car.Id);
            foreach(var image in images)
            {
               await fileService.DeleteFileAsync(UploadFolders.Cars, image.PhotoUrl);
                await imageRepository.DeleteAsync(image);
            }
                        
            await carRepository.DeleteAsync(car);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Car deleted successfully.");
        }
    }
}
