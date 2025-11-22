using RentalCar.Application.Values.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.CarAttributes.DTOS
{
    public class GetCarAttributesWithValuesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<ValueGetDto> Values { get; set; } = null!;
    }
}
