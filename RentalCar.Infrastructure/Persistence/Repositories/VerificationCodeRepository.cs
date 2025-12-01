using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using RentalCar.Infrastructure;

namespace RentalCar.Infrastructure.Persistence.Repositories;

public class VerificationCodeRepository(ApplicationDbContext context) : IVerificationCodeRepository
{
    public async Task<VerificationCode?> GetByEmailAsync(string email)
    {
        return await context.VerificationCodes
            .Where(code => code.NewEmail == email)
            .OrderByDescending(code => code.Expiration)
            .FirstOrDefaultAsync();
    }

    public async Task<VerificationCode?> GetByCodeAsync(string code)
    {
        return await context.VerificationCodes
            .FirstOrDefaultAsync(verificationCode => verificationCode.Code == code);
    }

    public async Task CreateAsync(VerificationCode verificationCode)
    {
        await context.VerificationCodes.AddAsync(verificationCode);
    }

    public Task UpdateAsync(VerificationCode verificationCode)
    {
        context.VerificationCodes.Update(verificationCode);
        return Task.CompletedTask;
    }

    public async Task<VerificationCode?> GetActiveByUserAsync(int userId)
    {
        return await context.VerificationCodes
         .Where(vc => vc.UserId == userId
                   && !vc.IsUsed
                   && vc.Expiration > DateTime.UtcNow)
         .OrderByDescending(vc => vc.CreatedAt) 
         .FirstOrDefaultAsync();
    }

    public async Task<VerificationCode?> GetActiveByUserAndHashAsync(int userId, string codeHash)
    {
        return await context.VerificationCodes
         .Where(vc => vc.UserId == userId
                   && vc.CodeHash == codeHash
                   && !vc.IsUsed
                   && vc.Expiration > DateTime.UtcNow)
         .SingleOrDefaultAsync();
    }

    public async Task<VerificationCode?> GetByUserAndEmailAsync(int userId, string email)
    {
        return await context.VerificationCodes
          .Where(vc => vc.UserId == userId && vc.NewEmail == email)
          .OrderByDescending(vc => vc.CreatedAt)
          .FirstOrDefaultAsync();
    }
}
