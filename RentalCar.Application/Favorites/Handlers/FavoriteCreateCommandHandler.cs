using Microsoft.AspNetCore.Http;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Favorites.Commands;
using RentalCar.Application.Favorites.Mappers;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System.Security.Claims;

namespace RentalCar.Application.Favorites.Handlers
{
    public class FavoriteCreateCommandHandler
        (IFavoriteRepository favoriteRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        ICarRepository carRepository) : ICommandHandler<FavoriteCreateCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(FavoriteCreateCommand command)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Result<string>.Fail("User not authenticated.", ErrorType.Unauthorized);

            int userId = int.Parse(userIdClaim.Value);

            var carExists = await carRepository.GetByIdAsync(command.CarId);
            if (carExists == null)
                return Result<string>.Fail("Car does not exist.", ErrorType.NotFound);

            var exists = await favoriteRepository.ExistsAsync(userId, command.CarId);
            if (exists)
                return Result<string>.Fail("Car already in favorites.", ErrorType.Conflict);

            var favorite = FavoriteMappers.ToEntity(userId, command.CarId);
            await favoriteRepository.AddAsync(favorite);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null,"Car added to favorites.");
        }
    }
}
