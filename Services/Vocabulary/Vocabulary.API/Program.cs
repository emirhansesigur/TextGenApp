using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application;
using Vocabulary.Application.Interfaces;
using Vocabulary.Infrastructure.Data;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception(".env dosyasından connection string okunamadı!");
}
// Console.WriteLine($"Connection String: {connectionString}"); // Test için

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VocabularyDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IVocabularyDbContext>(provider =>
    provider.GetRequiredService<VocabularyDbContext>());

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vocabulary API v1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
