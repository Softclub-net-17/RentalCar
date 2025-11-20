namespace RentalCar.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    // navigation
    public User User { get; set; } = null!;
    public Car Car { get; set; } = null!;
}