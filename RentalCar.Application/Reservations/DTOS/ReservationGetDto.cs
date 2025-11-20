namespace RentalCar.Application.Reservations.DTOS;

public class ReservationGetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? ReturnDate { get; set; }
}