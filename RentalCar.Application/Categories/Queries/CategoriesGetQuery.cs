using RentalCar.Application.Categories.DTOs;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Categories.Queries
{
    public class CategoriesGetQuery : IQuery<Result<List<CategoryGetDto>>>
    {

    }
}
