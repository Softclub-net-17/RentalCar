using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Favorites.DTOs;
using RentalCar.Application.Makes.DTOs;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Favorites.Mappers
{
    public static class FavoriteMappers
    {
        public static Favorite ToEntity(int userId, int carId)
        {
            return new Favorite
            {
                UserId = userId,
                CarId = carId
            };
        }

        public static async Task<List<FavoriteGetDto>> ToDtoAsync(
            this IEnumerable<Favorite> favorites,
            IReservationRepository reservationRepository)
        {
            var result = new List<FavoriteGetDto>();

            foreach (var f in favorites)
            {
                var carDto = (f.Car != null)
                    ? (await new List<Car> { f.Car }.ToDto(reservationRepository)).FirstOrDefault()
                    : null!;

                result.Add(new FavoriteGetDto
                {
                    UserId = f.UserId,
                    Car = carDto
                });
            }

            return result;
        }
    }
}
