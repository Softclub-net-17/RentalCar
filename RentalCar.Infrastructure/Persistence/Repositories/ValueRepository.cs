using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Infrastructure.Persistence.Repositories;

public class ValueRepository(ApplicationDbContext context) : IValueRepository
{
    public async Task<IEnumerable<Value>> GetAllAsync()
    {
        return await context.Values.ToListAsync();
    }

    public async Task<Value?> GetByIdAsync(int id)
    {
        return await context.Values.FindAsync(id);
    }

    public async Task CreateAsync(Value carValue)
    {
        await context.Values.AddAsync(carValue);
    }

    public Task UpdateAsync(Value carValue)
    {
        context.Values.Update(carValue);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Value carValue)
    {
        context.Values.Remove(carValue);
        return Task.CompletedTask;
    }
}