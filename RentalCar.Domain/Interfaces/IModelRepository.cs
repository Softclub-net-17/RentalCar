using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Interfaces
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAllAsync();
        Task<Model?> GetByIdAsync(int id);
        Task CreateAsync(Model model);
        Task UpdateAsync(Model model);
        Task DeleteAsync(Model model);
    }
}
