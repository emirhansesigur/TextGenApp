using DotNetEnv;
using Hangfire;
using MediatR;
using TextGen.Application.Commands.GenerateDailyTopics;
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
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddInfrastructureServices(connectionString);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureHangfire(connectionString);

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

app.UseHangfireDashboard();

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    // Her gün 10:00'da çalışır
    recurringJobManager.AddOrUpdate<IMediator>(
        "daily-topic-generation",
        mediator => mediator.Send(new GenerateDailyTopicsCommand(), CancellationToken.None),
        "00 10 * * *",
        new RecurringJobOptions { TimeZone = TimeZoneInfo.Local } // fix: Sunucu için Tr saat ayarı
    );
}

app.Run();