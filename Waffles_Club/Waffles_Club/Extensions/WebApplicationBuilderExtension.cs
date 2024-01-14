using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Waffles_Club.DataManagment;
using Waffles_Club.DataManagment.Implementations;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Implementations;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.Mappers;

namespace Waffles_Club.Extensions;

public static class WebApplicationBuilderExtension
{
	public static void AddServices(this WebApplicationBuilder builder)
    {
        //Default
		builder.Services.AddControllersWithViews();

        //Mappers
        builder.Services.AddScoped<StringToGuidMapper>();

		//Repository
		builder.Services.AddScoped<ICartRepository, CartRepository>();
		builder.Services.AddScoped<IOrderRepository, OrderRepository>();
		builder.Services.AddScoped<IOrderWaffleRepository, OrderWaffleRepository>();

		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();

        builder.Services.AddScoped<IWaffleRepository, WaffleRepository>();
        builder.Services.AddScoped<IWaffleTypeRepository, WaffleTypeRepository>();
		builder.Services.AddScoped<IFillingTypeRepository, FillingTypeRepository>();

        //Services
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddScoped<IRoleService, RoleService>();

        builder.Services.AddScoped<IWaffleService, WaffleService>();
        builder.Services.AddScoped<IWaffleTypeService, WaffleTypeService>();
		builder.Services.AddScoped<IFillingTypeService, FillingTypeService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICartService,CartService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
	}


	   public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt");
    
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
    
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });
            builder.Services.AddAuthorization(options => options.DefaultPolicy =
                new AuthorizationPolicyBuilder
                        (JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                });
            });
        }
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }
    
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        string? deviceConnection = builder.Configuration.GetConnectionString("ConnectionString");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(deviceConnection);
        });
    }
}