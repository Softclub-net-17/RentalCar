using RentalCar.Application.Common.Results;
using RentalCar.Application.Favorites.DTOs;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Favorites.Queries
{
    public class FavoritesGetQuery : IQuery<Result<List<FavoriteGetDto>>>
    {
        
    }
}
