using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Common.Security;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Enums;

namespace RentalCar.Infrastructure.Persistence.Seeds;

public class DefaultUsers
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var admin = await context.Users.FirstOrDefaultAsync(user => user.Email == "siyovush.azamov06@gmail.com");
        
        if (admin != null)  return;
        
        var passwordHash = PasswordHasher.HashPassword("12345");

        var newUser = new User
        {
            Email = "siyovush.azamov06@gmail.com",
            Role = Role.Admin,
            PasswordHash = passwordHash,
        };
        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
    }
}