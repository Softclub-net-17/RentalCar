using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Domain.Interfaces
{
    public interface IVerificationCodeRepository 
    {
        Task<VerificationCode> GetByEmailAsync(string email);
        Task<VerificationCode?> GetByCodeAsync(string code);
        Task CreateAsync(VerificationCode verificationCode);
        Task UpdateAsync(VerificationCode verificationCode);
    }
}
