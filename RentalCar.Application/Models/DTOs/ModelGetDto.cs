using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.DTOs
{
    public class ModelGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MakeId { get; set; }
    }
}
