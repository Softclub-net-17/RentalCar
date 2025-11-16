using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.Commands;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Users.Handlers;

public class UserDeleteCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UserDeleteCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UserDeleteCommand command)
    {
        var user = await userRepository.GetByIdAsync(command.Id);
        if (user == null)
        {
            return Result<string>.Fail("User not found.", ErrorType.NotFound);
        }

        await userRepository.DeleteAsync(user);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "User deleted successfully.");
    }
}