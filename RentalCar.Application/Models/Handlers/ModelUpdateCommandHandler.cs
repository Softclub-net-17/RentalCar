using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.Commands;
using RentalCar.Application.Models.Mappers;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Handlers
{
    public class ModelUpdateCommandHandler(
        IValidator<ModelUpdateCommand> validator,
        IUnitOfWork unitOfWork,
        IModelRepository repository) : ICommandHandler<ModelUpdateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(ModelUpdateCommand command)
        {
            var validations = validator.Validate(command);
            if (!validations.IsValid)
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var model = await repository.GetByIdAsync(command.Id);

            if (model == null)
            {
                return Result<string>.Fail("Model not found", ErrorType.NotFound);
            }

            command.MapFrom(model);

            await repository.UpdateAsync(model);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Model updated successfully");
        }
    }
}
