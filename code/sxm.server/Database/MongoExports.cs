using CitizenFX.Core;
using MongoDB.Bson;
using Sxm.MongoDB.Repositories.Collections.Users;

namespace Sxm.Server.Database;

public interface IMongoExports;

public sealed class MongoExports : IMongoExports
{
    public MongoExports(ExportDictionary exports, UserRepository users)
    {
        exports.Add("mongo_insert", (string json) =>
        {
            var doc = BsonDocument.Parse(json);

            if (doc is null)
                throw new Exception($"Document is null: \n{json}");

            if(!users.InsertDocument(doc))
                throw new Exception($"Failed to insert document: {doc.BsonType}");
        });
    }
}