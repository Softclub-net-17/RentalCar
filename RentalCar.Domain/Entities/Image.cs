using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public int? CarId { get; set; }
        public int? MakeId { get; set; }

        //navigations
        public Car? Car { get; set; }
        public Make? Make { get; set; }
    }
}
