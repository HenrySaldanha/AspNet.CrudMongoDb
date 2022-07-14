using MongoDB.Driver;

namespace Repository.MongoDb.Context
{
    public interface IMongoContext
    {
        IMongoDatabase GetDatabase();
    }
}