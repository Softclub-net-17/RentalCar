using RentalCar.Application.Models.Commands;
using RentalCar.Application.Models.DTOs;
using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Mappers
{
    public static class ModelMappers
    {
        public static Model ToEntity(this ModelCreateCommand command)
        {
            return new Model
            {
                Name = command.Name,
                MakeId = command.MakeId,
            };
        }

        public static void MapFrom(this ModelUpdateCommand command, Model model)
        {
            model.Name = command.Name;
            model.MakeId = command.MakeId;
        }

        public static List<ModelGetDto> ToDto(this IEnumerable<Model> models)
        {
            return models.Select(c => new ModelGetDto()
            {
                Id = c.Id,
                Name = c.Name,
                MakeId= c.MakeId,
            }).ToList();
        }
    }
}
