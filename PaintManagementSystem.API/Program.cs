using PaintManagementSystem.API.DataBase;
using Microsoft.EntityFrameworkCore;
using PaintManagementSystem.API.Repositories;
using PaintManagementSystem.API.Services;
using PaintManagementSystem.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<PaintProductRepository>();
builder.Services.AddScoped< IPaintProductsService, PaintProductsService>();

builder.Services.AddDbContext<PaintDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PaintManagementConnection")));

var app = builder.Build();

app.MapControllers();

app.Run();

