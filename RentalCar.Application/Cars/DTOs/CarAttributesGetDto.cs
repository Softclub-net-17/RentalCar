using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Cars.DTOs
{
    public class CarAttributesGetDto
    {
        public int AttributeId { get; set; }
        public int ValueId { get; set; }
        public string AttributeName { get; set; } = string.Empty;
        public string ValueName { get; set; } = string.Empty;
    }
}
