using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Models.Commands
{
    public class ModelDeleteCommand(int id) : ICommand<Result<string>>
    {
        public int Id { get; set; } = id;
    }
}
