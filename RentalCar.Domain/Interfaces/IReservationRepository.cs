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
    Task<List<Reservation>> GetMeActiveByCarIdAsync(int carId);
    Task<List<Reservation>> GetByUserIdAsync(int userId);
    Task<int> CountActiveAsync();
    Task<decimal> GetTodayRevenueAsync();
    Task<List<Reservation>> GetReservationsInRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<Reservation>> GetReservationsInPeriodAsync(DateTime startDate, DateTime endDate);

}