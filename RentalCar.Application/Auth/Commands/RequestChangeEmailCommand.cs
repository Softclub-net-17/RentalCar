using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Auth.Commands
{
    public class RequestChangeEmailCommand : ICommand<Result<string>>
    {
        public int UserId { get; set; }
        public string NewEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
