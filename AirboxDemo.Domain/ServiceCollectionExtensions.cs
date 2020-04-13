using AirboxDemo.Domain.DataLayer;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Authentication;

namespace AirboxDemo.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLocationDomainModel(this IServiceCollection services)
        {
            services.AddSingleton<ILocationDataAccess, DummyLocationDataAccess>();
        }

        public static void AddMongoClient(this IServiceCollection services, MongoUrl mongoUrl)
        {
            var mongoSettings = MongoClientSettings.FromUrl(mongoUrl);

            mongoSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            mongoSettings.GuidRepresentation = GuidRepresentation.Standard;

            services.AddSingleton<IMongoClient>(new MongoClient(mongoSettings));
        }
    }
}
