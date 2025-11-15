using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; }
        public bool Tinting { get; set; }
        public int Millage { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int ModelId { get; set; }

        //navigations
        public Model Model { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<CarValue> CarValues { get; set; }

    }
}
