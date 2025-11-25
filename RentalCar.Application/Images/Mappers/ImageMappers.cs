using RentalCar.Application.Images.Commands;
using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Images.Mappers
{
    public static class ImageMappers
    {
        public static Image ToCarImage(string filename,int carId)
        {
            return new Image()
            {
                PhotoUrl = filename,
                CarId = carId,
            };
        }
        public static Image ToMakeImage(string filename, int makeId)
        {
            return new Image()
            {
                PhotoUrl = filename,
                MakeId = makeId,
            };
        }
    }
}
