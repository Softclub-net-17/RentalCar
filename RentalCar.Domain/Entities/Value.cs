using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class Value
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CarAttributeId { get; set; }

        //navigations
        public CarAttribute CarAttribute { get; set; }

        public IEnumerable<CarValue> CarValues { get; set; }
    }
}
