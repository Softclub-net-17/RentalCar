using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.Commands;
using RentalCar.Application.Models.Mappers;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Handlers
{
    public class ModelCreateCommandHandler(
        IUnitOfWork unitOfWork, IModelRepository repository,
        IValidator<ModelCreateCommand> validator) : ICommandHandler<ModelCreateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(ModelCreateCommand command)
        {
            var validations = validator.Validate(command);
            if (!validations.IsValid)
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var model = command.ToEntity();

            await repository.CreateAsync(model);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Model created successfully");
        }
    }
}
