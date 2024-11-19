using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("UserDb")); // Für Tests: InMemoryDatabase

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>() // Identity mit User und Rollen
    .AddEntityFrameworkStores<ApplicationDbContext>() // Verbindung zur Datenbank herstellen
    .AddDefaultTokenProviders(); // Token (z. B. für Passwort-Reset)

// Optional: PasswordHasher für manuelles Hashing
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles(); // Aktiviert Default-Dateien wie index.html
app.UseStaticFiles();  // Aktiviert die Bereitstellung statischer Dateien
app.UseAuthorization();
app.MapControllers();



app.Run();
