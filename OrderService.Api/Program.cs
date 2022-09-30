using Microsoft.EntityFrameworkCore;
using OrderService.Api.Extentions;
using OrderService.Api.Middlewares;
using OrderService.Data.Contexts;
using OrderService.Domain.Enums;
using OrderService.Service.Helpers;
using OrderService.Service.Mappers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

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

// Custome service
builder.Services.AddCustomServices();

// Jwt 
builder.Services.AddJwtService(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger setup service
builder.Services.AddSwaggerService();

// Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

EnvironmentHelper.WebRootPath = 
    app.Services.GetService<IWebHostEnvironment>()?.WebRootPath;

//app.UseMiddleware<CustomExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
