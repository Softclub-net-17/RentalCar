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
    public class CategoryCreateCommandHandler
        (IValidator<CategoryCreateCommand> validator,
        IUnitOfWork unitOfWork,
        ICategoryRepository repository) 
        : ICommandHandler<CategoryCreateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(CategoryCreateCommand command)
        {
            var validations = validator.Validate(command);
            if(!validations.IsValid)
            {
                var errors = string.Join("; ", validations.Errors.Select(e => e));
                return Result<string>.Fail(errors, ErrorType.Validation);
            }

            var exists = await repository.ExistsAsync(command.Name);

            if(exists)
            {
                return Result<string>.Fail("The Category with this name already exists", ErrorType.Conflict);
            }

            var category = command.ToEntity();

            await repository.CreateAsync(category);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Category created successfully");

        }
    }
}
