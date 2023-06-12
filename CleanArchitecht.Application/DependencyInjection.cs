using CleanArchitecht.Application.Services.Authentication;
using CleanArchitecht.Application.Services.Authentication.Commands;
using CleanArchitecht.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArchitecht.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
            services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();

            return services;



        }
    }
}
