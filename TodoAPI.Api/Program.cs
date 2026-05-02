using Microsoft.EntityFrameworkCore;
using TodoAPI.Application.Algorithms;
using TodoAPI.Application.Interfaces;
using TodoAPI.Application.Services;
using TodoAPI.Domain.Entities;
using TodoAPI.Infrastructure.Data;
using TodoAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecting SQLite
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=todoapp.db"));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ISortingStrategy<TodoTask>, LexoRankMergeSort>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
