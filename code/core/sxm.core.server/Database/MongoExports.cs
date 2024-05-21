using CitizenFX.Core;
using MongoDB.Bson;
using Sxm.Core.Attributes;
using Sxm.MongoDB.Repositories.Collections.Users;

namespace Sxm.Core.Server.Database;

public sealed class MongoExports(UserRepository users) : IMongoExports
{
    [Export("mongo_insert")]
    private void MongoInsert(string json)
    {
        var doc = BsonDocument.Parse(json);

        if (doc is null)
        {
            Debug.WriteLine($"Document is null: \n{json}");
            return;
        }

        users.InsertDocument(doc);
    }
}