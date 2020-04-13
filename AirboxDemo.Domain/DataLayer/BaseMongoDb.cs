using MongoDB.Driver;

namespace AirboxDemo.Domain.DataLayer
{
    public abstract class BaseMongoDb : IMongoDb
    {
        private readonly IMongoDatabase database;

        public BaseMongoDb(IMongoClient mongoClient)
        {
            this.database = mongoClient.GetDatabase(this.DatabaseName);
            this.RegisterClassMaps();
        }

        protected abstract string DatabaseName { get; }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return this.database.GetCollection<T>(collectionName);
        }

        protected virtual void RegisterClassMaps()
        {
        }
    }
}
