using MongoDB.Driver;

namespace Sxm.MongoDB.Repositories;

public sealed class SxmDb(MongoClientSettings options) : ContextFactory("sxm", options);