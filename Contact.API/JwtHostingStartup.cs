using Contact.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

[assembly: HostingStartup(typeof(Contact.Data.JwtHostingStartup))]
namespace Contact.Data
{
    public class JwtHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<DataContext>(options => options.UseSqlite(context.Configuration.GetConnectionString("DbConnection")).EnableSensitiveDataLogging());
                services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
                services.Configure<IdentityOptions>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 9;
                });
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = true,
                            ValidateIssuer = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = context.Configuration["JwtSettings:Issuer"],
                            ValidAudience = context.Configuration["JwtSettings:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(context.Configuration["JwtSettings:SecretKey"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            });
        }
    }
}
