using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task CreateAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(Car car);
        Task<int> CountAsync();
        Task<int> CountAvailableAsync();
    }
}
