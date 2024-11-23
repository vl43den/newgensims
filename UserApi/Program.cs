using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using UserApi.Services;
using UserApi.Data;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add configuration for environment variables
builder.Configuration.AddEnvironmentVariables();

// Register DbContexts with connection strings from environment variables
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

// Add Swagger for API documentation (available in Development environment)
builder.Services.AddSwaggerGen();

// Add Redis Cache configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "UserApiSession:";
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));
builder.Services.AddScoped<ISessionService, RedisSessionService>();

// Add Health Checks for monitoring
builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        healthQuery: "SELECT 1;",
        name: "default_sqlserver")  // Specify a unique name
    .AddSqlServer(
        connectionString: builder.Configuration.GetConnectionString("IncidentConnection"),
        healthQuery: "SELECT 1;",
        name: "incident_sqlserver")  // Specify a unique name for the second connection
    .AddRedis(builder.Configuration.GetConnectionString("RedisConnection"));  // Health check for Redis

// Add Controllers
builder.Services.AddControllers();

// Build the app
var app = builder.Build();

// Listen on all network interfaces
app.Urls.Add("http://0.0.0.0:5000"); // Ensure app is listening on all interfaces for Docker

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI only in Development
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Add HTTPS redirection for production
    app.UseHttpsRedirection();
}

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map health check endpoint
app.MapHealthChecks("/health");

// Map API controllers
app.MapControllers();

// Run the application
app.Run();
