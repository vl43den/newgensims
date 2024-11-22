using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using UserApi.Services;
using UserApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure MSSQL Contexts
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IncidentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IncidentConnection")));

// Register Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register application services
builder.Services.AddScoped<IUserService, AuthService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

// Add Swagger generator
builder.Services.AddSwaggerGen();

// Add Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "UserApiSession:";
});

builder.Services.AddControllers();

// Configure the app
var app = builder.Build();

// Listen on all network interfaces
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
