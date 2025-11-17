using RentalCar.Application.Categories.Commands;
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
    public class CategoryActivateCommandHandler
        (ICategoryRepository repository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<CategoryActivateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(CategoryActivateCommand command)
        {
            var category = await repository.GetByIdAsync(command.Id);

            if(category == null)
            {
                return Result<string>.Fail("Category not found", ErrorType.NotFound);
            }

            category.IsActive = true;
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Category activated successfully");
        }
    }
}
