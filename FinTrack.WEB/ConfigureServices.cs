using Azure.Identity;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Infrastructure.Data;
using FinTrack.WEB.Middleware;
using FinTrack.WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ZymLabs.NSwag.FluentValidation;

namespace FinTrack.WEB
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            //services.AddExceptionHandler<CustomExceptionHandlerMiddleware>();

            services.AddRazorPages();

            services.AddScoped(provider =>
            {
                var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
                var loggerFactory = provider.GetService<ILoggerFactory>();

                return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
            });

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);


            return services;
        }
    }
}
