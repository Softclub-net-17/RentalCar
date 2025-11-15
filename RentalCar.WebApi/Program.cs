using RentalCar.Application;
using RentalCar.Infrastructure;
using RentalCar.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddConnectionConfigurations(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();


builder.Services.AddConnectionConfigurations(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();
