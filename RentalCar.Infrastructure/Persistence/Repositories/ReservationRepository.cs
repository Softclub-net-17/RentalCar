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
}