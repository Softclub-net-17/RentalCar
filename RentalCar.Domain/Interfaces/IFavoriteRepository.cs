using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddAsync(Favorite favorite);
        Task DeleteAsync(int userId, int carId);
        Task<bool> ExistsAsync(int userId, int carId);
        Task<List<Favorite>> GetUserFavoritesAsync(int userId);
    }
}
