using System;
using System.Collections.Generic;
using System.Dynamic;
using CitizenFX.Core;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Sxm.Core.Attributes;

namespace Sxm.MongoDB;

public sealed class MongoExports
{
    private readonly IMongoDatabase _db;

    public MongoExports()
    {
        var options = new MongoClientSettings
        {
            Server = new MongoServerAddress("localhost", 27017),
            ConnectTimeout = TimeSpan.FromSeconds(5),
            DirectConnection = true,
        };

        var client = new MongoClient(options);
        _db = client.GetDatabase("sxm");

        if (_db is null)
            Debug.WriteLine("Database not found: sxm");
    }

    [Export]
    public void InsertOne(string collectionName, object documentObj)
    {
        var collection = _db.GetCollection<BsonDocument>(collectionName);
        NotNull(collection);

        try
        {
            var document = BsonDocument.Create(documentObj);
            collection.InsertOne(document);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(InsertOne)}: {ex.Message}");
        }
    }

    [Export]
    public void DeleteOne(string collectionName, object filterObj)
    {
        var collection = _db.GetCollection<BsonDocument>(collectionName);
        NotNull(collection);

        try
        {
            var filter = BsonDocument.Create(filterObj);
            collection.DeleteOne(filter);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(DeleteOne)}: {ex.Message}");
        }
    }
    
    [Export]
    public void ReplaceOne(string collectionName, object filterObj, object replacementObj)
    {
        var collection = _db.GetCollection<BsonDocument>(collectionName);
        NotNull(collection);

        try
        {
            var filter = BsonDocument.Create(filterObj);
            var replacement = BsonDocument.Create(replacementObj);
            collection.ReplaceOne(filter, replacement);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(ReplaceOne)}: {ex.Message}");
        }
    }
    
    [Export]
    public void UpdateOne(string collectionName, object filterObj, object updateObj)
    {
        var collection = _db.GetCollection<BsonDocument>(collectionName);
        NotNull(collection);

        try
        {
            var filter = BsonDocument.Create(filterObj);
            var update = new BsonDocument("$set", BsonDocument.Create(updateObj));
            collection.UpdateOne(filter, update);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(UpdateOne)}: {ex.Message}");
        }
    }
    
    [Export]
    public string? Find(string collectionName, object filterObj)
    {
        var collection = _db.GetCollection<BsonDocument>(collectionName);
        NotNull(collection);

        try
        {
            var filter = BsonDocument.Create(filterObj);
            var result = collection.Find(filter).FirstOrDefault();

            return result?.ToJson();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(Find)}: {ex.Message}");
            return null;
        }
    }
    
    [Export]
    public bool Exists(string collectionName, object filterObj) => 
        Find(collectionName, filterObj) is not null;

    private static void NotNull(object? obj)
    {
        if (obj is null)
            throw new ArgumentNullException("Collection not found");
    }
}