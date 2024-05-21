using System;
using MongoDB.Driver;
using Sxm.DependencyInjection;
using Sxm.DependencyInjection.Extensions;

namespace Sxm.MongoDB.Extensions;

public static class MongoExtensions
{
    public static IServiceCollection UseMongoDb(this IServiceCollection services, Action<MongoClientSettings> configure)
    {
        var settings = new MongoClientSettings();
        configure(settings);
        services.AddSingleton(typeof(MongoClientSettings), typeof(MongoClientSettings), settings);
        return services;
    }
    
    public static IServiceCollection UseMongoDb(this IServiceCollection services, MongoClientSettings settings)
    {
        services.AddSingleton(typeof(MongoClientSettings), typeof(MongoClientSettings), settings);
        return services;
    }
}