using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Commands
{
    public class ChangeEmailCommand : ICommand<Result<string>>
    {
        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}
