using RentalCar.Application.Makes.Commands;
using RentalCar.Application.Makes.DTOs;
using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Makes.Mappers
{
    public static class MakeMapper
    {
        public static Make ToEntity(this MakeCreateCommand command)
        {
            return new Make
            {
                Name = command.Name,
                IsActive = true,

            };
        }

        public static void MapFrom(this MakeUpdateCommand command, Make make)
        {
            make.Name = command.Name;
        }

        public static List<MakeGetDto> ToDto(this IEnumerable<Make> makes)
        {
            return makes.Select(c => new MakeGetDto()
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
                Image = c.Image != null ? c.Image.PhotoUrl : null
            }).ToList();
        }
    }
}
