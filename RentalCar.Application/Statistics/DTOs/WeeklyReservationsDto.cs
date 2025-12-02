using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Statistics.DTOs
{
    public class WeeklyReservationsDto
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
