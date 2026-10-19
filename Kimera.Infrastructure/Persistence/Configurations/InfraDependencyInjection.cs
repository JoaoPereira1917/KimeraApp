using Kimera.Application.Interfaces;
using Kimera.Infrastructure.External.Tmdb;
using Kimera.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Kimera.Infrastructure.Persistence.Configurations
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddOptions<TmdbOption>()
                .Bind(configuration.GetSection(TmdbOption.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();
                services.AddHttpClient<ITmdbService, TmdbService>((serviceProvider, client) =>
                { var option = serviceProvider.GetRequiredService<IOptions<TmdbOption>>().Value; 
                client.BaseAddress = new Uri(option.BaseUrl); 
                client.Timeout = TimeSpan.FromSeconds(10);
                    client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                    client.DefaultRequestHeaders.Authorization = new 
                            System.Net.Http.Headers.AuthenticationHeaderValue
                                                ("Bearer", option.BearerToken);
                });
            return services;
        }
    }
}
