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
        public int? UserId { get; set; }
        public bool IsUsed { get; set; }
        public string NewEmail { get; set; } = string.Empty;
        public string CodeHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
