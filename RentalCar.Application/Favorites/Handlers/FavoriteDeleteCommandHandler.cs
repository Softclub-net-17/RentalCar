using Microsoft.AspNetCore.Http;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Favorites.Commands;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Favorites.Handlers
{
    public class FavoriteDeleteCommandHandler
        (IFavoriteRepository favoriteRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : ICommandHandler<FavoriteDeleteCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(FavoriteDeleteCommand command)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Result<string>.Fail("User not authenticated.", ErrorType.Unauthorized);

            int userId = int.Parse(userIdClaim.Value);

            
            var exists = await favoriteRepository.ExistsAsync(userId, command.CarId);
            if (!exists)
                return Result<string>.Fail("Favorite not found.", ErrorType.NotFound);

            
            await favoriteRepository.DeleteAsync(userId, command.CarId);

            
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Ok(null,"Car removed from favorites.");
        }
    }
}
