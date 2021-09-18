using Contact.Data;
using Contact.DTO.AutoMapper;
using Contact.Logic;
using Contact.Logic.UploadImage;
using Contact.Logic.Validators.Filters;
using Contact.Logic.Validators.ModelValidator;
using Contact.Models.DTOs;
using Contact.Repository.Implementaions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Contact.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(Configuration);
            services.ConfigureIdentity();
            services.AddScoped<Seeder>();
            services.AddAutoMapper(typeof(Mapper));
            
            services.ConfigureJWt(Configuration);
            services.ConfigureSession();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthenticator, Authenticator>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IImageUploader, ImageUploader>();

            services.Configure<ImageSettingDTO>(Configuration.GetSection("Cloudingary"));

            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });

            services.AddControllers(o => o.Filters.Add<ValidationFilter>())
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contact.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder appSeed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact.API v1"));
            }

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            //appSeed.SeedAdminAsync().Wait(); // adds user with admin roles
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
