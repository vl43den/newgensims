using Microsoft.EntityFrameworkCore;
using UserApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Use the connection string from appsettings.json
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));  // Reference the connection string from appsettings.json

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseAuthorization();

app.MapControllers();

app.Run();
