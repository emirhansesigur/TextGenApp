using Auth.Application.DependencyInjection;
using Auth.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Environment variable 'DB_CONNECTION_STRING' okunamadı");
} //Console.WriteLine($"Connection String: {connectionString}"); // Test için

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
