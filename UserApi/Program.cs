using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using UserApi.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure SQLite Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); // This registers the IPasswordHasher<User> service

// Add the Swagger generator
builder.Services.AddSwaggerGen(); // Add this line to register Swagger generator

// Retrieve the Jwt Secret Key from the configuration with a fallback to an empty string
var secretKey = builder.Configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey", "The JWT secret key is missing in the configuration.");
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add controllers to the services collection (required for handling HTTP requests)
builder.Services.AddControllers();  // Ensure this line is included

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();  // Make sure the context name matches here
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Ensure database is created and migrations are applied
        context.Database.Migrate();

        // Create Admin role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Create test admin user if it doesn't exist
        if (!context.Users.Any())
        {
            var user = new User
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                Name = "Admin User",
                IsActive = true,
                Role = UserRole.Admin  // Use the UserRole enum here instead of a string
            };

            await userManager.CreateAsync(user, "Admin123!");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enable Swagger middleware
    app.UseSwaggerUI();  // Enable Swagger UI for interactive API docs
}

app.UseHttpsRedirection();

// Add Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();  // Ensure this line is included to map controller actions to endpoints

app.Run();
