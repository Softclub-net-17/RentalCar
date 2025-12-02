using RentalCar.Domain.Entities;

namespace RentalCar.Domain.Interfaces;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task CreateAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(Reservation reservation);
    Task<bool> IsCarBusy(int carId, DateTime start, DateTime end);
    Task<List<Reservation>> GetActiveByCarIdAsync(int carId);
    Task<int> CountActiveAsync();
    Task<decimal> GetTodayRevenueAsync();

}