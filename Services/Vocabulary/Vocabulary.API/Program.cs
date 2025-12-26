using DotNetEnv;
using Vocabulary.Application.DependencyInjection; 
using Vocabulary.Infrastructure.DependencyInjection; 

if (File.Exists(".env"))
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Environment variable 'DB_CONNECTION_STRING' okunamadı");
} //Console.WriteLine($"Connection String: {connectionString}"); // Test için

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructureServices(connectionString);
builder.Services.AddApplicationServices();


var app = builder.Build();


//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vocabulary API v1");
//});

app.UseAuthorization();

app.MapControllers();

app.Run();
