namespace RentalCar.WebApi.Extensions
{
    public static class CorsConfigurations
    {
        public static void AddCorsConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins(
                            "https://localhost:3000", 
                            "https://localhost:3001",
                            "http://localhost:3000",  
                            "http://localhost:3001")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
        }
    }
}
