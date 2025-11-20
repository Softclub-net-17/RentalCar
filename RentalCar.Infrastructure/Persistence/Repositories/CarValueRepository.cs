using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Infrastructure.Persistence.Repositories;

public class CarValueRepository(ApplicationDbContext context) : ICarValueRepository
{
    public async Task CreateAsync(CarValue carValue)
    {
        await context.CarValues.AddAsync(carValue);
    }

    public Task UpdateAsync(CarValue carValue)
    {
         context.CarValues.Update(carValue);
         return Task.CompletedTask;
    }
    
    public async Task<CarValue?> GetByIdAsync(int carId, int valueId)
    {
        return await context.CarValues.FirstOrDefaultAsync(x => x.CarId == carId && x.ValueId == valueId);
    }
    
    public async Task<bool> AnyAsync(int carId, int valueId)
    {
        return await context.CarValues.AnyAsync(x => x.CarId == carId && x.ValueId == valueId);
    }

    public Task DeleteAsync(CarValue carValue)
    {
        context.CarValues.Remove(carValue);
        return Task.CompletedTask;
    }
}