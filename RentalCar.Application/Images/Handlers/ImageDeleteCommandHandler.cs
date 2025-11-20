using RentalCar.Application.Common.Results;
using RentalCar.Application.Images.Commands;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Images.Handlers
{
    public class ImageDeleteCommandHandler(
        ICarImageRepository repository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<ImageDeleteCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(ImageDeleteCommand command)
        {
            var image = await repository.GetByIdAsync(command.Id);
            if (image == null)
                return Result<string>.Fail("Image not found", ErrorType.NotFound);

            await repository.DeleteAsync(image);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null,"Image deleted");
        }
    }
}
