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
    public class ModelRepository(ApplicationDbContext context) : IModelRepository
    {
        public async Task CreateAsync(Model model)
        {
            await context.Models.AddAsync(model);
        }

        public Task DeleteAsync(Model model)
        {
            context.Models.Remove(model);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await context.Models.ToListAsync();
        }

        public async Task<Model?> GetByIdAsync(int id)
        {
            return await context.Models.FindAsync(id);
        }

        public async Task<List<Model>> GetByMakeIdAsync(int makeId)
        {
            return await context.Models.Where(m => m.MakeId == makeId)
                .ToListAsync();
        }
        
        public async Task<List<Model>> GetModelsByMakeWithCarsAsync(int makeId)
        {
            return await context.Models
                .Where(m => m.MakeId == makeId && m.Cars.Any()) 
                .Include(m => m.Cars)
                .ToListAsync();
        }

        public Task UpdateAsync(Model model)
        {
            context.Models.Update(model);
            return Task.CompletedTask;
        }
    }
}