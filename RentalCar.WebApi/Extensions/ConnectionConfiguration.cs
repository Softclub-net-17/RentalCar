using Microsoft.EntityFrameworkCore;
using RentalCar.Infrastructure;

namespace RentalCar.WebApi.Extensions
{
    public static class ConnectionConfiguration
    {
        public static void AddConnectionConfigurations(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
