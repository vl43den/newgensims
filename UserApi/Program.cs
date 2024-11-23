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

// Add Swagger for API documentation (available in all environments)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "User API",
        Version = "v1",
        Description = "API for managing users and incidents."
    });
});

// Add Redis Cache configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "UserApiSession:";
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));
builder.Services.AddScoped<ISessionService, RedisSessionService>();

// Add Controllers
builder.Services.AddControllers();

// Build the app
var app = builder.Build();

// Ensure app is listening on all interfaces (useful for Docker)
app.Urls.Add("http://0.0.0.0:5000");
// Configure middleware pipeline

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "User API v1");
    options.RoutePrefix = "swagger"; // This will ensure Swagger UI is accessible at /swagger
});

// Add HTTPS redirection for production (optional)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Run the application
app.Run();
