using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.DTOS;
using RentalCar.Application.Users.Mappers;
using RentalCar.Application.Users.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Users.Handlers;

public class UserGetByIdQueryHandler(IUserRepository userRepository) : IQueryHandler<UserGetByIdQuery, Result<UserGetDto>>
{
    public async Task<Result<UserGetDto>> HandleAsync(UserGetByIdQuery command)
    {
        var user = await userRepository.GetByIdAsync(command.Id);

        if (user == null)
            return Result<UserGetDto>.Fail("User attribute not found.", ErrorType.NotFound);

        var item = user.ToDto();

        return Result<UserGetDto>.Ok(item);
    }
}