using TextGen.Application.Interfaces;
using TextGen.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

//if (string.IsNullOrEmpty(connectionString))
//{
//    throw new Exception(".env dosyasından connection string okunamadı!");
//}
// Console.WriteLine($"Connection String: {connectionString}"); // Test için

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ITextGenDbContext>(provider =>
    provider.GetRequiredService<TextGenDbContext>());

var app = builder.Build();


//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vocabulary API v1");
//});

app.UseAuthorization();

app.MapControllers();

app.Run();
