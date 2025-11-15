using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Infrastructure.Persistence.Repositories;

public class CarAttributeRepository(ApplicationDbContext context) : ICarAttributeRepository
{
    public async Task<IEnumerable<CarAttribute>> GetAllAsync()
    {
        return await context.CarAttributes.ToListAsync();
    }

    public async Task<CarAttribute?> GetByIdAsync(int id)
    {
        return await context.CarAttributes.FindAsync(id);
    }

    public async Task CreateAsync(CarAttribute carAttribute)
    {
        await context.CarAttributes.AddAsync(carAttribute);
    }

    public Task UpdateAsync(CarAttribute carAttribute)
    {
        context.CarAttributes.Update(carAttribute);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(CarAttribute carAttribute)
    {
        context.CarAttributes.Remove(carAttribute);
        return Task.CompletedTask;
    }
}