using Microsoft.EntityFrameworkCore;
using newgensims.Models;
using newgensims.Services;

var builder = WebApplication.CreateBuilder(args);

// Use the connection string from appsettings.json
builder.Services.AddDbContext<IncidentDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services in the DI container
builder.Services.AddScoped<IIncidentService, IncidentService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseAuthorization();

app.MapControllers();

app.Run();
