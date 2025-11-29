using RentalCar.Application;
using RentalCar.Infrastructure;
using RentalCar.Infrastructure.Persistence.Seeds;
using RentalCar.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfigurations();
builder.Services.AddAuthConfigurations(builder.Configuration);
builder.Services.AddConnectionConfigurations(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCorsConfigurations();
builder.WebHost.UseUrls("http://0.0.0.0:5049");
builder.Services.AddConnectionConfigurations(builder.Configuration);

var app = builder.Build();

try
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    await DefaultUsers.SeedAsync(context);
}
catch (Exception e)
{
    Console.WriteLine($"An error occurred while seeding the db: {e.Message}");
}

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{ 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    { 
        options.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API");
        options.SwaggerEndpoint("/swagger/client/swagger.json", "Client API");
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
