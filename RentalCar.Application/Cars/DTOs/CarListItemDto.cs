namespace RentalCar.Application.Cars.DTOs;

public class CarListItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal PricePerHour { get; set; }
}