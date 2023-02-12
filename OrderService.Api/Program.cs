using Microsoft.EntityFrameworkCore;
using OrderService.Api.Extentions;
using OrderService.Api.Middlewares;
using OrderService.Data.Contexts;
using OrderService.Domain.Enums;
using OrderService.Service.Helpers;
using OrderService.Service.Mappers;
using Serilog;

// Create a web application builder and add services to it
var builder = WebApplication.CreateBuilder(args);

// Add a database context using the Npgsql provider and the connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("DefaultConnection")));

// Add AutoMapper for object mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add authorization policies based on user roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin)));

    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.Admin)));

    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.Customer)));

    options.AddPolicy("AllPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.Chef),
        Enum.GetName(UserRole.Customer)));

    options.AddPolicy("ChefPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.SuperAdmin),
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.Chef)));
});

// Add custom services
builder.Services.AddCustomServices();

// Add JWT authentication service
builder.Services.AddJwtService(builder.Configuration);

// Add services for accessing HTTP context, controllers, and API explorer
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger service for API documentation
builder.Services.AddSwaggerService();

// Add Serilog for logging
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Build the application
var app = builder.Build();

// If the environment is in development, use Swagger for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Set the web root path for the environment helper
EnvironmentHelper.WebRootPath = 
    app.Services.GetService<IWebHostEnvironment>()?.WebRootPath;

//app.UseMiddleware<CustomExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
