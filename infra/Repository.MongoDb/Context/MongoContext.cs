using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repository.MongoDb.Options;

namespace Repository.MongoDb.Context;
public class MongoContext : IMongoContext
{
    private readonly MongoOptions _mongoOptions;
    private readonly MongoClient _mongoClient;

    public MongoContext(IOptions<MongoOptions> options)
    {
        _mongoOptions = options.Value;
        _mongoClient = new MongoClient(_mongoOptions.ConnectionString);
    }

    public IMongoDatabase GetDatabase() => _mongoClient.GetDatabase(_mongoOptions.Database);
}