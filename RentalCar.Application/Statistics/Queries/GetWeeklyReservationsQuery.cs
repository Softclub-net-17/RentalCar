using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Statistics.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Statistics.Queries
{
    public class GetWeeklyReservationsQuery : IQuery<Result<List<WeeklyReservationsDto>>>
    {

    }
}
