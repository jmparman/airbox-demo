using MongoDB.Driver;

namespace AirboxDemo.Domain.DataLayer
{
    public interface IMongoDb
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
