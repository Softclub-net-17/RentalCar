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
    public class GetRentalStatisticsQueryHandler(ICarRepository carRepository,
        IUserRepository userRepository, IReservationRepository reservationRepository) : IQueryHandler<GetRentalStatisticsQuery, Result<StatisticsDto>>
    {
        public async Task<Result<StatisticsDto>> HandleAsync(GetRentalStatisticsQuery query)
        {
            var totalCars = await carRepository.CountAsync();
            var availableCars = await carRepository.CountAvailableAsync();
            var countActiveCars = await reservationRepository.CountActiveAsync();
            var getTodayRevenue = await reservationRepository.GetTodayRevenueAsync();
            var countUsers = await userRepository.CountAsync();

            var dto = new StatisticsDto
            {
                TotalCars = totalCars,
                AvailableCars = availableCars,
                UserCount = countUsers,
                TotalRevenue = getTodayRevenue,
                ActiveCars = countActiveCars
            };

            return Result<StatisticsDto>.Ok(dto);
        }
    }
}
