using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Queries
{
    public class ModelsGetQuery : IQuery<Result<List<ModelGetDto>>>
    {
    }
}
