using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Statistics.DTOs;
using RentalCar.Application.Statistics.Queries;
using RentalCar.Domain.Interfaces;
using System.Globalization;

namespace RentalCar.Application.Statistics.Handlers
{
    public class GetRevenueTrendQueryHandler
        (IReservationRepository reservationRepository) : IQueryHandler<GetRevenueTrendQuery, Result<List<RevenueTrendDto>>>
    {
        public async Task<Result<List<RevenueTrendDto>>> HandleAsync(GetRevenueTrendQuery query)
        {
            var today = DateTime.UtcNow.Date;

            
            var startDate = DateTime.SpecifyKind(new DateTime(today.Year, 1, 1), DateTimeKind.Utc);

            
            var endDate = DateTime.SpecifyKind(
                new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month))
                    .AddDays(1).AddTicks(-1),
                DateTimeKind.Utc
            );

            var reservations = await reservationRepository.GetReservationsInPeriodAsync(startDate, endDate);

            var grouped = reservations
                .GroupBy(r => new { r.StartDate.Year, r.StartDate.Month })
                .Select(g => new RevenueTrendDto
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    Revenue = g.Sum(r => r.TotalPrice)
                })
                .ToList();

            
            var months = Enumerable.Range(1, today.Month)
                .Select(m => new DateTime(today.Year, m, 1))
                .ToList();

            var result = months
                .Select(m => new RevenueTrendDto
                {
                    Month = m.ToString("MMMM", CultureInfo.InvariantCulture),
                    Revenue = grouped.FirstOrDefault(x => x.Month == m.ToString("MMMM", CultureInfo.InvariantCulture))?.Revenue ?? 0
                })
                .ToList();

            return Result<List<RevenueTrendDto>>.Ok(result);
        }
    }
}
