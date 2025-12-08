using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Ocelot.json dosyasını konfigürasyona ekle
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2. Ocelot servisini ekle
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// 3. Ocelot middleware
await app.UseOcelot();

app.Run();