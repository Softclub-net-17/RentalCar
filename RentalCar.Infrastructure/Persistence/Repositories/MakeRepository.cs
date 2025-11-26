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
    public class MakeRepository(ApplicationDbContext context) : IMakeRepository
    {

        public async Task CreateAsync(Make make)
        {
            await context.Makes.AddAsync(make);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await context.Makes.AnyAsync(m => m.Name == name);
        }

        public async Task<IEnumerable<Make>> GetAllAsync()
        {
            return await context.Makes.Include(i => i.Image).ToListAsync();
        }

        public async Task<Make?> GetByIdAsync(int id)
        {
            return await context.Makes.FindAsync(id);
        }

        public Task UpdateAsync(Make make)
        {
            context.Update(make);
            return Task.CompletedTask;
        }
    }
}
