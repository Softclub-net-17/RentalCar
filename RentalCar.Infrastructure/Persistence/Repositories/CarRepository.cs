using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Infrastructure.Persistence.Repositories
{
    public class CarRepository(ApplicationDbContext context) : ICarRepository
    {
        public async Task CreateAsync(Car car)
        {
             await context.Cars.AddAsync(car);
        }

        public Task DeleteAsync(Car car)
        {
            context.Cars.Remove(car);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await context.Cars.Include(c => c.Images).ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await context.Cars.FindAsync(id);
        }

        public Task UpdateAsync(Car car)
        {
             context.Cars.Update(car);
            return Task.CompletedTask;
        }
    }
}
