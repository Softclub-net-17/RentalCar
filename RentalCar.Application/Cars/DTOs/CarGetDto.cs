using RentalCar.Application.Reservations.DTOS;

namespace RentalCar.Application.Cars.DTOs
{
    public class CarGetDto 
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool Tinting { get; set; }
        public int Millage { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int ModelId { get; set; }
        public List<string> Images { get; set; } = null!;
        public List<CarAttributesGetDto> CarAttributes { get; set; } = null!;
        public List<CarBusyDateDto> BusyDates { get; set; } = new();

    }
}