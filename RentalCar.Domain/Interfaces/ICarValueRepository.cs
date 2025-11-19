using RentalCar.Domain.Entities;

namespace RentalCar.Domain.Interfaces;

public interface ICarValueRepository
{
    Task CreateAsync(CarValue carValue);
    Task UpdateAsync(CarValue carValue);
    Task<CarValue?> GetByIdAsync(int carId, int valueId);
    Task DeleteAsync(CarValue carValue);
}