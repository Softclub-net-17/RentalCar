using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Images.Commands
{
    public class ImageDeleteCommand(int id) : ICommand<Result<string>>
    {
        public int Id { get; set; } = id;
    }
}
