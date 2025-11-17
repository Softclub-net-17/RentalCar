using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.DTOS;
using RentalCar.Application.Users.Mappers;
using RentalCar.Application.Users.Queries;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Users.Handlers;

public class UserGetQueryHandler(IUserRepository userRepository) : IQueryHandler<UserGetQuery, Result<List<UserGetDto>>>
{
    public async Task<Result<List<UserGetDto>>> HandleAsync(UserGetQuery query)
    {
        var user = await userRepository.GetAllAsync();
        var item = user.ToDto();
        
        return Result<List<UserGetDto>>.Ok(item);
    }
}