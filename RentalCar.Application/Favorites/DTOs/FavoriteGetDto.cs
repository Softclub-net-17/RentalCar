using RentalCar.Application.Cars.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Favorites.DTOs
{
    public class FavoriteGetDto
    {
        public int UserId { get; set; }
        public CarGetDto Car { get; set; } = null!;
    }
}
