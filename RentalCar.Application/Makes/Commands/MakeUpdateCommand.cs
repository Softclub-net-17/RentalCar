using Microsoft.AspNetCore.Http;
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

namespace RentalCar.Application.Makes.Commands
{
    public class MakeUpdateCommand : ICommand<Result<string>>
    {
        [SwaggerIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? Picture { get; set; } = null;
    }
}
