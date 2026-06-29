using PaintManagementSystem.API.DataBase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PaintDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PaintManagementConnection")));

var app = builder.Build();

app.MapControllers();

app.Run();

