using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using API.Helpers;
using System.ComponentModel.DataAnnotations;
using API.SignalR;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
             services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ITokenService, TokenService>();
            ////services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<ILikesRepository, LikesRepository>();
            //services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            services.AddScoped<IPhotoService,PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddSignalR();
            services.AddSingleton<PresenceTracker>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}