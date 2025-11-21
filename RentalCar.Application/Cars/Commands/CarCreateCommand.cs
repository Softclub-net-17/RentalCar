using Microsoft.AspNetCore.Http;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Cars.Commands
{
    public class CarCreateCommand : ICommand<Result<string>>
    {
        public decimal PricePerHour { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; }
        public bool Tinting { get; set; }
        public int Millage { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int ModelId { get; set; }
        public List<int> ValueIds { get; set; } = [];
        public List<IFormFile> Pictures { get; set; } = [];

    }
}