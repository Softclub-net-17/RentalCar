using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.VerificationCodes.Mappers
{
    public static class VerificationCodeMappers
    {
        public static VerificationCode ToEntity(string code, string email)
        {
            return new VerificationCode
            {
                Email = email,
                Code = code,
                Expiration = DateTime.UtcNow.AddMinutes(2),
                IsUsed = false
            };
        }

        public static void DeActivate(this VerificationCode code)
        {
            code.IsUsed = true;
        }
    }
}
