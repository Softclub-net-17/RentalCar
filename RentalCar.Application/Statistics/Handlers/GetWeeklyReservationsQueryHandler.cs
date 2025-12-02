using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Statistics.DTOs;
using RentalCar.Application.Statistics.Queries;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Statistics.Handlers
{
    public class GetWeeklyReservationsQueryHandler
        (IReservationRepository reservationRepository
        ) : IQueryHandler<GetWeeklyReservationsQuery, Result<List<WeeklyReservationsDto>>>
    {
        public async Task<Result<List<WeeklyReservationsDto>>> HandleAsync(GetWeeklyReservationsQuery query)
        {
                var today = DateTime.UtcNow.Date;
                var startDate = today.AddDays(-7);
                var endDate = today.AddDays(1).AddTicks(-1);

                var reservations = await reservationRepository.GetReservationsInRangeAsync(startDate, endDate);

                var grouped = reservations
                    .GroupBy(r => r.StartDate.DayOfWeek)
                    .Select(g => new WeeklyReservationsDto
                    {
                        DayOfWeek = g.Key.ToString(),
                        Count = g.Count()
                    })
                    .ToList();

                
                var orderedDays = Enumerable.Range(0, 7)
                    .Select(offset => startDate.AddDays(offset).DayOfWeek)
                    .ToList();

                var result = orderedDays
                    .Select(d => new WeeklyReservationsDto
                    {
                        DayOfWeek = d.ToString(),
                        Count = grouped.FirstOrDefault(x => x.DayOfWeek == d.ToString())?.Count ?? 0
                    })
                    .ToList();

                return Result<List<WeeklyReservationsDto>>.Ok(result);
            }
        }
    }
