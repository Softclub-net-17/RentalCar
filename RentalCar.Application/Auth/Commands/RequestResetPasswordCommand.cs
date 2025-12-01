using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalCar.Application.Auth.Commands
{
    public class RequestResetPasswordCommand : ICommand<Result<string>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
