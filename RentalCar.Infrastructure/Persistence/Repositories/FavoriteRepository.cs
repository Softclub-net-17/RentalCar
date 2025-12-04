using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Infrastructure.Persistence.Repositories
{
    public class FavoriteRepository(ApplicationDbContext context) : IFavoriteRepository
    {
        public async Task AddAsync(Favorite favorite)
        {
            await context.Favorites.AddAsync(favorite); 
        }

        public async Task DeleteAsync(int userId, int carId)
        {
            var favorite = await context.Favorites
             .FirstOrDefaultAsync(f => f.UserId == userId && f.CarId == carId);

            if (favorite != null)
                context.Favorites.Remove(favorite);
        }

        public async Task<bool> ExistsAsync(int userId, int carId)
        {
            return await context.Favorites
               .AnyAsync(f => f.UserId == userId && f.CarId == carId);
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(int userId)
        {
            return await context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Car)
                    .ThenInclude(c => c.Images)
                .Include(f => f.Car)
                    .ThenInclude(c => c.CarValues)
                        .ThenInclude(cv => cv.Value)
                            .ThenInclude(v => v.CarAttribute)
                .Include(f => f.Car)
                    .ThenInclude(c => c.Reservations)
                .ToListAsync();
        }
    }
}
