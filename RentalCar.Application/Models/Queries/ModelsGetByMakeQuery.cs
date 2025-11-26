using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Models.DTOs;

namespace RentalCar.Application.Models.Queries;

public class ModelsGetByMakeQuery(int makeId) : IQuery<Result<List<ModelGetDto>>>
{
    public int MakeId { get; } = makeId;
}