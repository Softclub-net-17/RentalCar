using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Favorites.Commands
{
    public class FavoriteCreateCommand(int carId) : ICommand<Result<string>>
    {
        public int CarId { get; set; } = carId;
    }
}
