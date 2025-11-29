using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;

namespace RentalCar.Application.Email.Commands;

public class SendEmailCommand : ICommand<Result<string>>
{
    public List<int> ReceiverIds { get; set; } = [];
    public string Subject { get; set; } = "";
    public string Body { get; set; } = "";
}
