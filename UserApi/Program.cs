using UserApi.Models;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Use SQLite instead of SQL Server for the database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services if needed (like Swagger, authentication, etc.)

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection(); // Ensures HTTPS is used

app.MapControllers(); // Maps the API controllers to the correct routes

app.Run();

