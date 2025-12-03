using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Favorites.Commands
{
    public class FavoriteDeleteCommand(int carId) : ICommand<Result<string>>
    {
        public int CarId { get; set; } = carId;
    }
}