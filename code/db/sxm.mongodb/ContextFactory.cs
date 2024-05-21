using MongoDB.Driver;

namespace Sxm.MongoDB;

public abstract class ContextFactory : IContextFactory
{
    public string Name { get; }
    public IMongoDatabase Database { get; }

    protected ContextFactory(string databaseName, MongoClientSettings options)
    {
        Name = databaseName;
        
        var dbClient = new MongoClient(options);
        Database = dbClient.GetDatabase(Name);
    }
}