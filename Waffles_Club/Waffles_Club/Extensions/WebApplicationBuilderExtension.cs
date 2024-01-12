using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Waffles_Club.DataManagment;
using Waffles_Club.DataManagment.Implementations;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.Extensions;

public static class WebApplicationBuilderExtension
{
	public static void AddServices(this WebApplicationBuilder builder)
    {
        //Default
		builder.Services.AddControllersWithViews();

		//Repository
		builder.Services.AddScoped<ICartRepository, CartRepository>();
		builder.Services.AddScoped<IOrderRepository, OrderRepository>();
		builder.Services.AddScoped<IOrderWaffleRepository, OrderWaffleRepository>();

		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();

        builder.Services.AddScoped<IWaffleRepository, WaffleRepository>();
        builder.Services.AddScoped<IWaffleTypeRepository, IWaffleTypeRepository>();
		builder.Services.AddScoped<IFillingTypeRepository, FillingTypeRepository>();
	}


	public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
                    ValidAudience = builder.Configuration["Jwt:Audience"]!,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
                };
            });
        builder.Services.AddAuthorization(options => options.DefaultPolicy =
            new AuthorizationPolicyBuilder
                    (JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
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