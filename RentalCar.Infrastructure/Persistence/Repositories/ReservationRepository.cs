using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Infrastructure.Persistence.Repositories;

public class ReservationRepository(ApplicationDbContext context) :  IReservationRepository
{
    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await context.Reservations.ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await context.Reservations.FindAsync(id);
    }

    public  async Task CreateAsync(Reservation reservation)
    {
        await context.Reservations.AddAsync(reservation);
    }

    public Task UpdateAsync(Reservation reservation)
    {
        context.Reservations.Update(reservation);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Reservation reservation)
    {
        context.Reservations.Remove(reservation);
        return Task.CompletedTask;
    }

    public async Task<bool> IsCarBusy(int carId, DateTime start, DateTime end)
    {
        return await context.Reservations.AnyAsync(r =>
            r.CarId == carId &&
            ((start >= r.StartDate && start <= r.EndDate) ||
             (end >= r.StartDate && end <= r.EndDate) ||
             (start <= r.StartDate && end >= r.EndDate)));
    }

    public async Task<int> CountActiveAsync()
    {
        return await context.Reservations.
            CountAsync(r => r.StartDate <= DateTimeOffset.UtcNow
         && r.EndDate >= DateTimeOffset.UtcNow
         && r.ReturnDate == null);
    }

    public async Task<decimal> GetTodayRevenueAsync()
    {
        var today = DateTime.UtcNow.Date; 
        var tomorrow = today.AddDays(1); 

        return await context.Reservations
            .Where(r => r.StartDate >= today && r.StartDate < tomorrow && r.ReturnDate != null)
            .SumAsync(r => r.TotalPrice);
    }
    
    public async Task<List<Reservation>> GetActiveByCarIdAsync(int carId)
    {
        var now = DateTime.UtcNow;
        return await context.Reservations
            .Where(r => r.CarId == carId && r.EndDate >= now)
            .ToListAsync();
    }

    public async Task<List<Reservation>> GetReservationsInRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await context.Reservations
        .Where(r => r.StartDate >= startDate && r.StartDate <= endDate)
        .ToListAsync();
    }

    public async Task<List<Reservation>> GetReservationsInPeriodAsync(DateTime startDate, DateTime endDate)
    {
        startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

        return await context.Reservations
            .Where(r => r.StartDate >= startDate && r.StartDate <= endDate)
            .ToListAsync();
    }
}