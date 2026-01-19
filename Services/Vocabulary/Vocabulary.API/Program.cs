using DotNetEnv;
using Vocabulary.Application.DependencyInjection; 
using Vocabulary.Infrastructure.DependencyInjection; 

if (File.Exists(".env"))
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
var jwtSecretKey = Environment.GetEnvironmentVariable("JwtSecretKey");

if (string.IsNullOrEmpty(connectionString)) throw new Exception("DB_CONNECTION_STRING eksik!");
if (string.IsNullOrEmpty(jwtSecretKey)) throw new Exception("JWT_SECRET_KEY eksik!");

builder.Services.AddControllers();

builder.Services.AddInfrastructureServices(connectionString, jwtSecretKey);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseAuthentication(); // Token geçerli mi?
app.UseAuthorization();  // Yetkisi var mı?

app.MapControllers();

app.Run();