using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Common.Security;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Enums;

namespace RentalCar.Infrastructure.Persistence.Seeds;

public class DefaultUsers
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await EnsureAdminAsync(context, "siyovush.azamov06@gmail.com", "123456");
        await EnsureAdminAsync(context, "muhammadkhojaev187@gmail.com", "slojniy");
    }

    private static async Task EnsureAdminAsync(ApplicationDbContext context, string email, string password)
    {
        var admin = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        var passwordHash = PasswordHasher.HashPassword(password);

        if (admin == null)
        {
            admin = new User
            {
                Email = email,
                Role = Role.Admin,
                PasswordHash = passwordHash,
            };
            await context.Users.AddAsync(admin);
        }
        else
        {
            // обновляем пароль и роль, если админ уже есть
            admin.Role = Role.Admin;
            admin.PasswordHash = passwordHash;
            context.Users.Update(admin);
        }

        await context.SaveChangesAsync();
    }
}