using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Cars.DTOs
{
    public class CarGetDto 
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; }
        public bool Tinting { get; set; }
        public int Millage { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int ModelId { get; set; }
        public List<string> Values { get; set; } = new();
        public List<string> Images { get; set; } = new();
    }
}