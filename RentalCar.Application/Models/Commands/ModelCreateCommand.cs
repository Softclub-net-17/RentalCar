using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Models.Commands
{
    public class ModelCreateCommand : ICommand<Result<string>>
    {
        public string Name { get; set; } = string.Empty;
        public int MakeId { get; set; }
    }
}
