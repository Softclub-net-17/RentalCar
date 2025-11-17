using RentalCar.Application.Categories.Commands;
using RentalCar.Application.Categories.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Categories.Handlers
{
    public class CategoryUpdateCommandHandler
        (IValidator<CategoryUpdateCommand> validator,
        IUnitOfWork unitOfWork,
        ICategoryRepository repository)
        : ICommandHandler<CategoryUpdateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(CategoryUpdateCommand command)
        {
            var validations = validator.Validate(command);
            if(!validations.IsValid)
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var category = await repository.GetByIdAsync(command.Id);

            if (category == null)
            {
                 return Result<string>.Fail("Category not found", ErrorType.NotFound);
            }

            var exists = await repository.ExistsAsync(command.Name);
            if (exists)
            {
                return Result<string>.Fail("Category with this name already exists", ErrorType.Conflict);
            }

            command.MapFrom(category);

            await repository.UpdateAsync(category);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Category updated successfully");

        }
    }
}
