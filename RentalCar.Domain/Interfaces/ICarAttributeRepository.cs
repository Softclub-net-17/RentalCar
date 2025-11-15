using RentalCar.Domain.Entities;

namespace RentalCar.Domain.Interfaces;

public interface ICarAttributeRepository
{
    Task<IEnumerable<CarAttribute>> GetAllAsync();
    Task<CarAttribute?> GetByIdAsync(int id);
    Task CreateAsync(CarAttribute carAttribute);
    Task UpdateAsync(CarAttribute carAttribute);
    Task DeleteAsync(CarAttribute carAttribute);
}