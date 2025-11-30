namespace RentalCar.Domain.ValueObject;

public class CarFilter
{
    public int? MakeId { get; set; }
    public int? ModelId { get; set; }
    public string? EngineType { get; set; } 
    public int? MileageFrom { get; set; }
    public int? MileageTo { get; set; }
    public string? BodyType { get; set; }
    public string? Transmission { get; set; }
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public List<int>? AttributeValueIds { get; set; }
}