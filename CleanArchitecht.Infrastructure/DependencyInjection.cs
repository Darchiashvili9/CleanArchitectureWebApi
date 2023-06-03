using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Application.Common.Interfaces.Services;
using CleanArchitecht.Infrastructure.Authentication;
using CleanArchitecht.Infrastructure.Persistence;
using CleanArchitecht.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
