
namespace RentalCar.Application.Auth.DTOs
{
    public class RequestChangeEmailDto
    {
        public string NewEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
