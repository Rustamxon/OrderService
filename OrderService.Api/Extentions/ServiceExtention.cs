using OrderService.Service.Interfaces.CustomerService;
using OrderService.Service.Interfaces.ICategorieService;
using OrderService.Service.Interfaces.IOrderService;
using OrderService.Service.Interfaces.IPaymentService;
using OrderService.Service.Interfaces.IProductServices;
using OrderService.Service.Services;
using OrderService.Service.Services.CustomerServices;
using OrderService.Service.Services.OrderServices;
using OrderService.Service.Services.PaymentServices;
using Microsoft.OpenApi.Models;
using OrderService.Data.IRepositories;
using OrderService.Data.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OrderService.Service.Interfaces;

namespace OrderService.Api.Extentions;

public static class ServiceExtention
{
    #region DI container
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IOrderService, OrdersService>();
        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAttachmentService, AttachmentService>();
    }
    #endregion

    #region Jwt service
    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(p =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            p.SaveToken = true;
            p.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddScoped<IAuthService, AuthService>();
    }
    #endregion

    #region Swagger setup service
    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(p =>
        {
            p.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "OrderService",
                Version = "v1",
                Description = "Luboy",
                Contact = new OpenApiContact()
                {
                    Name = "asd"
                }
            });

            p.ResolveConflictingActions(ad => ad.First());
            p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Token ni Bearer sozidan song bu yerga yozing"
            });

            p.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        #endregion
    }
}

