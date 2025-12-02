using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class CarAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //navigations
        public IEnumerable<Value> Values { get; set; } = null!;
    }
}
