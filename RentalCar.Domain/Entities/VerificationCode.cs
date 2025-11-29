using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Entities
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
