using RentalCar.Domain.Entities;

namespace RentalCar.Domain.Interfaces
{
    public interface IMakeRepository
    {
        Task<IEnumerable<Make>> GetAllAsync();
        Task<Make?> GetByIdAsync(int id);
        Task CreateAsync(Make make);
        Task UpdateAsync(Make make);
        Task<bool> ExistsAsync(string name);
    }
}