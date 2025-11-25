namespace RentalCar.Application.Cars.DTOs;

public class CarFilterGetDto
{
    public int? MakeId { get; set; }
    public int? ModelId { get; set; }
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public List<int>? AttributeValueIds { get; set; }
}