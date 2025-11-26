using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Statistics.DTOs
{
    public class StatisticsDto
    {
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int ActiveCars { get; set; }
        public int UserCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
