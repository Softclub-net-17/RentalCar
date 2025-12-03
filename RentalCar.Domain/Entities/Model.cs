using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MakeId { get; set; }

        //navigations
        public Make Make { get; set; } = null!;
        public List<Car> Cars { get; set; } = [];
    }
}
