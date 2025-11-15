using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class CarValue
    {
        public int CarId { get; set; }
        public int ValueId { get; set; }

        //navigations
        public Car Car { get; set; }
        public Value Value { get; set; }
    }
}
