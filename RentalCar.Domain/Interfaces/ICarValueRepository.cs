using RentalCar.Domain.Entities;

namespace RentalCar.Domain.Interfaces;

public interface ICarValueRepository
{
    Task CreateAsync(CarValue carValue);
    Task UpdateAsync(CarValue carValue);
    Task<IEnumerable<CarValue>> GetByCarIdAsync(int carId); 
    Task<CarValue?> GetByIdAsync(int carId, int valueId);
    Task<bool> AnyAsync(int carId, int valueId);
    Task DeleteAsync(CarValue carValue);
}