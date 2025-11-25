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
    public class ImageRepository(ApplicationDbContext context) : IImageRepository
    {
        public async Task CreateAsync(Image image)
        {
            await context.Images.AddAsync(image);
        }

        public Task DeleteAsync(Image image)
        {
            context.Remove(image);
            return Task.CompletedTask;
        }

        public async Task<List<Image>> GetByCarId(int carId)
        {
            return await context.Images.Where(carid => carid.CarId == carId).ToListAsync();
            
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            return await context.Images.FindAsync(id);
        }

        public async Task<Image?> GetByMakeId(int makeId)
        {
            return await context.Images.Where(m => m.MakeId == makeId).FirstOrDefaultAsync();
        }
    }
}
