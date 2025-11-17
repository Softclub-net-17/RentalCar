using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Makes.Commands;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Makes.Handlers
{
    public class MakeActivateCommandHandler(IUnitOfWork unitOfWork,
        IMakeRepository repository
        ) : ICommandHandler<MakeActivateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(MakeActivateCommand command)
        {
            var make = await repository.GetByIdAsync(command.Id);

            if (make == null)
            {
                return Result<string>.Fail("Make not found", ErrorType.NotFound);
            }

            await repository.ActivateAsync(make);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok("Make activated successfully");
        }
    }
}
