using RentalCar.Application.Common.Results;
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
    public class MakeUpdateCommandHandler
        (IValidator<MakeUpdateCommand> validator,
        IUnitOfWork unitOfWork,
        IMakeRepository makeRepository) : ICommandHandler<MakeUpdateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(MakeUpdateCommand command)
        {
            var validations = validator.Validate(command);
            if(!validations.IsValid)
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var make = await makeRepository.GetByIdAsync(command.Id);

            if (make == null)
            {
                return Result<string>.Fail("Make not found", ErrorType.NotFound);
            }

            var exists = await makeRepository.ExistsAsync(command.Name);
            if (exists)
            {
                return Result<string>.Fail("Make with this name already exists", ErrorType.Conflict);
            }

            command.MapFrom(make);

            await makeRepository.UpdateAsync(make);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null,"Make updated successfully");
        }
    }
}
