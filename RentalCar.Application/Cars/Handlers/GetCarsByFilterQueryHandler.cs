using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Mappers;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Cars.Handlers;

public class GetCarsByFilterQueryHandler(
    ICarRepository carRepository) 
    : IQueryHandler<GetCarsByFilterQuery, Result<List<CarListItemDto>>>
{

    public async Task<Result<List<CarListItemDto>>> HandleAsync(GetCarsByFilterQuery request)
    {
        var f = request.Filter;

        var query = carRepository.Query();

        if (f.MakeId.HasValue)
            query = query.Where(c => c.Model.MakeId == f.MakeId);

        if (f.ModelId.HasValue)
            query = query.Where(c => c.ModelId == f.ModelId);

        if (f.YearFrom.HasValue)
            query = query.Where(c => c.Year >= f.YearFrom);

        if (f.YearTo.HasValue)
            query = query.Where(c => c.Year <= f.YearTo);

        if (f.PriceFrom.HasValue)
            query = query.Where(c => c.PricePerHour >= f.PriceFrom);

        if (f.PriceTo.HasValue)
            query = query.Where(c => c.PricePerHour <= f.PriceTo);

        if (f.AttributeValueIds != null && f.AttributeValueIds.Any())
        {
            query = query.Where(c =>
                c.CarValues.Any(v => f.AttributeValueIds.Contains(v.ValueId)));
        }

        var cars = await query.ToListAsync();

        var items = cars.ToFilter();

        return Result<List<CarListItemDto>>.Ok(items);
    }
}