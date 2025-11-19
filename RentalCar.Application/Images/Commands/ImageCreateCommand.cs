using Microsoft.AspNetCore.Http;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Images.Commands
{
    public class ImageCreateCommand : ICommand<Result<string>>
    {
        public List<IFormFile> Files { get; set; } 
        public int CarId { get; set; }
    }
}
