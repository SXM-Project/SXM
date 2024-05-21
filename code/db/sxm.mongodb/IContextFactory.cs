using MongoDB.Driver;

namespace Sxm.MongoDB;

public interface IContextFactory
{
    /// <summary>
    /// The database name.
    /// </summary>
    string Name { get; }

    IMongoDatabase Database { get; }
}