using Web.Services;
using Web.Services.Abstract;

namespace Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMoviesService, MoviesService>();
            
            return services;
        }
    }
}
