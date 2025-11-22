using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.Commands;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Handlers
{
    public class ModelDeleteCommandHandler(IUnitOfWork unitOfWork,
        IModelRepository modelRepository) : ICommandHandler<ModelDeleteCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(ModelDeleteCommand command)
        {
            var model = await modelRepository.GetByIdAsync(command.Id);
            if (model == null)
            {
                return Result<string>.Fail("Model not found.", ErrorType.NotFound);
            }

            await modelRepository.DeleteAsync(model);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null,"Model deleted successfully.");
        }
    }
}
