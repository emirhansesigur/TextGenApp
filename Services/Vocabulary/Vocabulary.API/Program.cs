using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Services;
using Vocabulary.Core.Entities;
using Vocabulary.Core.Repositories;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<VocabularyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IGenericRepository<WordList>, GenericRepository<WordList>>();
builder.Services.AddScoped<IWordListRepository, WordListRepository>();

// Register Services
builder.Services.AddScoped<WordListService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/openapi/v1.json", "My App");
    //});


}

app.UseAuthorization();

app.MapControllers();

app.Run();
