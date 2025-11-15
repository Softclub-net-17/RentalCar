using RentalCar.Domain.Entities;

namespace RentalCar.Domain.Interfaces;

public interface IValueRepository
{
    Task<IEnumerable<Value>> GetAllAsync();
    Task<Value?> GetByIdAsync(int id);
    Task CreateAsync(Value carValue);
    Task UpdateAsync(Value carValue);
    Task DeleteAsync(Value carValue);
}