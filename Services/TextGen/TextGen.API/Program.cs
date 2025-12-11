using DotNetEnv;
using TextGen.Application.DependencyInjection;
using TextGen.Application.Services;
using TextGen.Infrastructure.DependencyInjection;
using TextGen.Infrastructure.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// --- Yapılandırma ve Temel Ayarlar (Config) ---
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception(".env dosyasından connection string okunamadı!");
}// Console.WriteLine($"Connection String: {connectionString}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddInfrastructureServices(connectionString);
builder.Services.AddApplicationServices();


// --- Dış Servisler ve HTTP İstemcileri (Infrastructure Layer) ---
builder.Services.AddHttpClient<IVocabularyService, VocabularyService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vocabulary API v1");
//});

app.UseAuthorization();
app.MapControllers();

app.Run();