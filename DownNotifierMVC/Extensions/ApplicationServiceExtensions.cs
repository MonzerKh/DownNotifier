using AccessLayer.Data;
using AutoMapper;
using BusinessLayer.BackServices;
using BusinessLayer.BusinessUnits;
using BusinessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelsLayer.AutoMapperProfiles;
using ModelsLayer.Business;
using System.Runtime;

namespace DownNotifierMVC.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration Config)
        {
            services.Configure<AppSettings>(Config.GetSection("AppSettings"));
            services.AddControllers();
            //services.AddScoped<ITokenService, TokenService>();
          
            IServiceCollection serviceCollection = services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(
                Config.GetConnectionString("DefaultConnection")
                ));
            
            services.AddAutoMapper(cfg => {
                cfg.AddProfile(new DataToDtoMappingProfile());
            });


            services.AddTransient<ApplicationDbContext>();

            services.AddScoped<EmailHelper>();
         //   services.AddScoped<ServiceActivator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
