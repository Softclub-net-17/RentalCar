using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Domain.ValueObject;

namespace RentalCar.Infrastructure.Persistence.Repositories
{
    public class CarRepository(ApplicationDbContext context) : ICarRepository
    {
        public async Task<List<Car>> GetByFilterAsync(CarFilter filter)
        {
            var query = context.Cars
                .Include(c => c.Model)
                .ThenInclude(m => m.Make)
                .Include(c => c.CarValues)
                .ThenInclude(cv => cv.Value).ThenInclude(v => v.CarAttribute)
                .AsQueryable();

            if (filter.MakeId.HasValue)
                query = query.Where(c => c.Model.MakeId == filter.MakeId);

            if (filter.ModelId.HasValue)
                query = query.Where(c => c.ModelId == filter.ModelId);

            if (filter.YearFrom.HasValue)
                query = query.Where(c => c.Year >= filter.YearFrom);

            if (filter.YearTo.HasValue)
                query = query.Where(c => c.Year <= filter.YearTo);

            if (filter.PriceFrom.HasValue)
                query = query.Where(c => c.PricePerHour >= filter.PriceFrom);

            if (filter.PriceTo.HasValue)
                query = query.Where(c => c.PricePerHour <= filter.PriceTo);

            if (filter.AttributeValueIds != null && filter.AttributeValueIds.Any())
                query = query.Where(c => c.CarValues.Any(v => filter.AttributeValueIds.Contains(v.ValueId)));

            return await query.ToListAsync();

        }

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
            return await context.Cars
                .Include(c => c.Images)
                .Include(c => c.CarValues)
                .ThenInclude(cv => cv.Value)
                .ThenInclude(ca => ca.CarAttribute)
                .ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await context.Cars
                .Include(c => c.Images)
                .Include(c => c.CarValues)
                .ThenInclude(cv => cv.Value)
                .ThenInclude(ca => ca.CarAttribute)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public IQueryable<Car> Query()
        {
            return context.Cars
                .Include(c => c.Model)
                .ThenInclude(m => m.Make)
                .Include(c => c.Images)
                .Include(c => c.CarValues)
                .ThenInclude(cv => cv.Value)
                .ThenInclude(v => v.CarAttribute)
                .AsQueryable();
        }

        public Task UpdateAsync(Car car)
        {
            context.Cars.Update(car);
            return Task.CompletedTask;
        }
    }
}