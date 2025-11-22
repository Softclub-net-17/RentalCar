using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Makes.DTOs;
using RentalCar.Application.Makes.Mappers;
using RentalCar.Application.Makes.Queries;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Makes.Handlers
{
    public class MakeGetQueryHandler
        (IMakeRepository repository) : IQueryHandler<MakeGetQuery, Result<List<MakeGetDto>>>
    {
        public async Task<Result<List<MakeGetDto>>> HandleAsync(MakeGetQuery query)
        {
            var makes = await repository.GetAllAsync();
            var items = makes.ToDto();

            return Result<List<MakeGetDto>>.Ok(items);
        }
    }
}
