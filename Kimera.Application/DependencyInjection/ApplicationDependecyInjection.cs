using Kimera.Application.Interfaces;
using Kimera.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Kimera.Application.DependencyInjection
{
    public static class ApplicationDependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            return services;
        }
    }
}
