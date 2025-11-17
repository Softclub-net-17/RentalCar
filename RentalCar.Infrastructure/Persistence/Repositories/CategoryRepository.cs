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
    internal class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {

        public async Task CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public Task UpdateAsync(Category category)
        {
            context.Update(category);
            return Task.CompletedTask;
        }
    }
}
