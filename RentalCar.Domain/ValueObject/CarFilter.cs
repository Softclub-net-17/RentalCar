namespace RentalCar.Domain.ValueObject;

public class CarFilter
{
    public int? MakeId { get; set; }
    public int? ModelId { get; set; }
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public List<int>? AttributeValueIds { get; set; }
}