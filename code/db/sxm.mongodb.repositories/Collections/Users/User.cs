using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sxm.MongoDB.Repositories.Collections.Users;

public sealed class User
{
    [BsonId, BsonRepresentation(BsonType.String)]
    public ulong Id { get; set; }
}