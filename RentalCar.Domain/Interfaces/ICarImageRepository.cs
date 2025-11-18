using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Interfaces
{
    public interface ICarImageRepository
    {
        Task<Image?> GetByIdAsync(int id);
        Task CreateAsync(Image image);
        Task DeleteAsync(Image image);

    }
}
