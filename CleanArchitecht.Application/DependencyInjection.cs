using CleanArchitecht.Application.Services.Authentication;
using CleanArchitecht.Application.Services.QuizService;
using CleanArchitecht.Application.Services.QuotesService;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArchitecht.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IQuotesService, QuotesService>();
            services.AddScoped<IQuizService, QuizService>();

            return services;



        }
    }
}
