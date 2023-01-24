using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tebeshir.Art.Authentication.DataLayer;
using Microsoft.EntityFrameworkCore;
using Tebeshir.Art.Authentication.Business.Services.Interface;
using Tebeshir.Art.Authentication.Business.Services;
using Microsoft.AspNetCore.Identity;
using Tebeshir.Art.Authentication.Domain.Models;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Tebeshir.Art.Authentication.Infrastructure
{
    public static class DataLayerRegistrationService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 3;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<UserDatabaseContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
       .AddJwtBearer(options =>
       {
           options.SaveToken = true;
           options.RequireHttpsMetadata = false;
           options.TokenValidationParameters = new TokenValidationParameters()
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateIssuerSigningKey = true,
               ValidateLifetime = true,
               ValidIssuer = configuration["Jwt:ValidIssuer"],
               ValidAudience = configuration["Jwt:ValidAudience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
               ClockSkew = TimeSpan.Zero, // Messes with expiry!
           };
       });


            services.AddTransient<IClaimsService, ClaimsService>();


            services.AddTransient<IJwtTokenService, JwtTokenService>();


            return services;
        }
    }
}