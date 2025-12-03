using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{           
    public class Favorite
    {       
        public int UserId { get; set; }
        public int CarId { get; set; }
        public User User { get; set; } = null!;
        public Car Car { get; set; } = null!;   
    }
}
