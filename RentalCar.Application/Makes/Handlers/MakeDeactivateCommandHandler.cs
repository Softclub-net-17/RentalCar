using RentalCar.Application.Common.Constants;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Makes.Commands;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Makes.Handlers
{
    public class MakeDeactivateCommandHandler
        (IMakeRepository repository,
        IUnitOfWork unitOfWork,
        IImageRepository imageRepository,
        IFileService fileService) : ICommandHandler<MakeDeactivateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(MakeDeactivateCommand command)
        {
            var make = await repository.GetByIdAsync(command.Id);
            if (make == null)
                return Result<string>.Fail("Make not found", ErrorType.NotFound);

            var image = await imageRepository.GetByMakeId(make.Id); 
            if (image != null)
            {
                await fileService.DeleteFileAsync(UploadFolders.Makes, image.PhotoUrl);
                await imageRepository.DeleteAsync(image);
            }

            make.IsActive = false;
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null,"Make deactivated successfully");
        }
    }
}
