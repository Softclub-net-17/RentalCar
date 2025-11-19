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

    public async Task DeleteAsync(CarValue carValue)
    {
        var existing = await context.CarValues
            .FirstOrDefaultAsync(x =>
                x.CarId == carValue.CarId &&
                x.ValueId == carValue.ValueId);

        if (existing is null)
            return;

        context.CarValues.Remove(existing);
    }
}