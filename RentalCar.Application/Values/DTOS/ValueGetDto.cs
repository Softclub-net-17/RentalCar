namespace RentalCar.Application.Values.DTOS;

public class ValueGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CarAttributeId { get; set; }
}