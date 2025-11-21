using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Cars.Queries
{
    public class CarGetQuery : IQuery<Result<List<CarGetDto>>>
    {

    }
}
