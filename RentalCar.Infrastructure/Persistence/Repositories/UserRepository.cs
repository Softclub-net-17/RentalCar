using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Common.Security;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }
    public async Task<IEnumerable<User>> GetUsersByIdsAsync(List<int> ids)
    {
        return await context.Users.Where(user => ids.Any(id => id == user.Id)).ToListAsync();
    }
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await context.Users.AnyAsync(user => user.Email == email);
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        context.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user)
    {
        context.Users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task<int> CountAsync()
    {
        return await context.Users.CountAsync();    
    }

    public async Task<bool> VerifyPasswordAsync(int userId, string password)
    {
        var user = await GetByIdAsync(userId);
        if (user == null)
         return false;

        if (string.IsNullOrWhiteSpace(user.PasswordHash))
            return false;

        var isValid = PasswordHasher.Verify(password, user.PasswordHash);
        return isValid;
    }
}