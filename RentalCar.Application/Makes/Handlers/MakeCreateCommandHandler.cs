using Microsoft.AspNetCore.Http;
using RentalCar.Application.Common.Constants;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Images.Mappers;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Makes.Commands;
using RentalCar.Application.Makes.Mappers;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Makes.Handlers
{
    public class MakeCreateCommandHandler(IValidator<MakeCreateCommand> validator,
        IMakeRepository repository,
        IUnitOfWork unitOfWork,
        IFileService fileService,
        IImageRepository imageRepository)
        : ICommandHandler<MakeCreateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(MakeCreateCommand command)
        {
            var validations = validator.Validate(command);
            if(!validations.IsValid )
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var exists = await repository.ExistsAsync(command.Name);
            if(exists)
            {
                return Result<string>.Fail("Name already exists", ErrorType.Conflict);
            }

            var make = command.ToEntity();

            await repository.CreateAsync(make);
            await unitOfWork.SaveChangesAsync();

            if (command.Picture != null)
            {
                var fileName = await fileService.SaveFileAsync(UploadFolders.Makes, command.Picture);
                var image = ImageMappers.ToMakeImage(fileName, make.Id);
                await imageRepository.CreateAsync(image);
            }

            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null, "Make created successfully");
        }
    }
}
