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
    public class ImageRepository(ApplicationDbContext context) : ICarImageRepository
    {
        public async Task CreateAsync(Image image)
        {
            await context.Images.AddAsync(image);
        }

        public  Task DeleteAsync(Image image)
        {
            context.Remove(image);
            return Task.CompletedTask;
        }
        public async Task<Image?> GetByIdAsync(int id)
        {
            return await context.Images.FindAsync(id);
        }

    }
}
