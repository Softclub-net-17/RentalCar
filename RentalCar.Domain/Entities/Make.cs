using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }

        //navigations
        public Category Category { get; set; }
        public List<Model> Models { get; set; }
    }
}
