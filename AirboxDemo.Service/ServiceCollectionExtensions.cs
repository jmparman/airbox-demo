using Microsoft.Extensions.DependencyInjection;

namespace AirboxDemo.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGoogleService(this IServiceCollection services)
        {
            services.AddSingleton<IGeolocationApiService, GeolocationApiService>();
        }
    }
}
