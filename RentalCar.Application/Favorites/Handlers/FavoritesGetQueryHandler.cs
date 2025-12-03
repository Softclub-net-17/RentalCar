using Microsoft.AspNetCore.Http;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Favorites.DTOs;
using RentalCar.Application.Favorites.Mappers;
using RentalCar.Application.Favorites.Queries;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using System.Security.Claims;

namespace RentalCar.Application.Favorites.Handlers
{
    public class FavoritesGetQueryHandler
        (IFavoriteRepository favoriteRepository,
        IHttpContextAccessor httpContextAccessor,
        IReservationRepository reservationRepository) : IQueryHandler<FavoritesGetQuery, Result<List<FavoriteGetDto>>>
    {
        public async Task<Result<List<FavoriteGetDto>>> HandleAsync(FavoritesGetQuery query)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Result<List<FavoriteGetDto>>.Fail("User not authenticated.", ErrorType.Unauthorized);

            int userId = int.Parse(userIdClaim.Value);

            var favorites = await favoriteRepository.GetUserFavoritesAsync(userId);

            var dtoList = await favorites.ToDtoAsync(reservationRepository);

            return Result<List<FavoriteGetDto>>.Ok(dtoList);
        }
    }
}
