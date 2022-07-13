using MongoDB.Driver;

namespace Repository.MongoDb.Context
{
    public interface IMongoContext
    {
        public IMongoDatabase GetDatabase();
    }
}