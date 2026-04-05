using Microsoft.EntityFrameworkCore;
using fityou.Infrastructure.Persistence;
using fityou.Endpoints;
using fityou.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ExerciseService>();

/*Dependency injection (DI) container*/
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlite("Data Source=fityou.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.MapExerciseEndpoints();

app.Run();
