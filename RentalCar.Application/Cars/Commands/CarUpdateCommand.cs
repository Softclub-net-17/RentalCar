using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Cars.Commands
{
    public class CarUpdateCommand : ICommand<Result<string>>
    {
        [SwaggerIgnore]
        public int Id { get; set; }
        public decimal PricePerHour { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; }
        public bool Tinting { get; set; }
        public int Millage { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int ModelId { get; set; }
        public List<int> ValueIds { get; set; } = [];
        public List<IFormFile>? Pictures { get; set; } = null!;

    }
}