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
        public Task ActivateAsync(Make make)
        {
            make.IsActive = true;
            return Task.CompletedTask;
        }

        public async Task CreateAsync(Make make)
        {
            await context.Makes.AddAsync(make);
        }

        public Task DeactivateAsync(Make make)
        {
            make.IsActive = false;
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await context.Makes.AnyAsync(m => m.Name == name);
        }

        public async Task<IEnumerable<Make>> GetAllAsync()
        {
            return await context.Makes.ToListAsync();
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
