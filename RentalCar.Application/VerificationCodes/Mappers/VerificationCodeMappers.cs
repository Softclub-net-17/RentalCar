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
        public static VerificationCode ToEntity(string email, string codeHash, int expiryMinutes = 5)
        {
            return new VerificationCode
            {
                NewEmail = email,
                CodeHash = codeHash,
                Expiration = DateTime.UtcNow.AddMinutes(expiryMinutes),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void DeActivate(this VerificationCode code)
        {
            code.IsUsed = true;
        }

        public static void UpdateFrom(this VerificationCode entity,string codeHash, int expiryMinutes = 5)
        {
            entity.CodeHash = codeHash;
            entity.Expiration = DateTime.UtcNow.AddMinutes(expiryMinutes);
            entity.IsUsed = false;
            entity.CreatedAt = DateTime.UtcNow;
        }
    }
}
