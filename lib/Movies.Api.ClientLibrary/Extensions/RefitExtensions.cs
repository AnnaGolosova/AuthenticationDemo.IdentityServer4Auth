using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Api.ClientLibrary.Configuration;
using Refit;

namespace Movies.Api.ClientLibrary.Extensions
{
    public static class RefitExtensions
    {
        public static IServiceCollection AddRefitWithConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(MoviesApiHttpConfiguration.SectionName);
            services.Configure<MoviesApiHttpConfiguration>(configSection);

            var moviesConfiguration = configSection.Get<MoviesApiHttpConfiguration>();
            
            services.AddRefitClient<IMoviesApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(moviesConfiguration.MovieApiRoute));
            
            return services;
        }
    }
}
