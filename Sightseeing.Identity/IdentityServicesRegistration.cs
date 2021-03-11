
using Sightseeing.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Sightseeing.Identity.Models;
using Sightseeing.Application.Contracts.Identity;
using Sightseeing.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Sightseeing.Identity
{
    public static class IdentityServicesRegistration
    {
        public static void RegisterIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<SightseeingIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SightseeingIdentityConnectionString"), 
                    o => o.MigrationsAssembly(typeof(SightseeingIdentityDbContext).Assembly.FullName));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<SightseeingIdentityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            var jwtSection = configuration.GetSection("JwtSettings");
            var jwtSettings = new JwtSettings();
            jwtSection.Bind(jwtSettings);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(opt =>
                    {
                        opt.RequireHttpsMetadata = false;
                        opt.SaveToken = false;
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                        };
                    });
        }
    }
}
