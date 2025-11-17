using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Interfaces
{
    public interface IMakeRepository
    {
        Task<IEnumerable<Make>> GetAllAsync();
        Task<Make?> GetByIdAsync(int id);
        Task CreateAsync(Make make);
        Task UpdateAsync(Make make);
        Task DeactivateAsync(Make make);
        Task ActivateAsync(Make make);
        Task<bool> ExistsAsync(string name);
    }
}
