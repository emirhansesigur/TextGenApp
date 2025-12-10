using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TextGen.Application;
using TextGen.Application.Interfaces;
using TextGen.Application.Services;
using TextGen.Infrastructure.Data;
using TextGen.Infrastructure.Services;

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

builder.Services.AddScoped<ITextGenDbContext>(provider =>
    provider.GetRequiredService<TextGenDbContext>());

// Vocabulary Servisi için HttpClient Ayarı
builder.Services.AddHttpClient<IVocabularyService, VocabularyService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
});

// 1. ILlmClient arayüzünü, somut LlmClient sınıfıyla eşleştiriyoruz.
builder.Services.AddScoped<TextGen.Application.Services.ILlmClient, TextGen.Infrastructure.Services.LlmClient>();

// 2. PromptBuilder sınıfını da kaydetmeniz gerekiyor (Handler constructor'ında var).
builder.Services.AddTransient<TextGen.Application.Services.PromptBuilder>();

builder.Services.AddDbContext<TextGenDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();


//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vocabulary API v1");
//});



app.UseAuthorization();

app.MapControllers();

app.Run();