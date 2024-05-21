using MongoDB.Driver;
using Sxm.Core.Client;
using Sxm.DependencyInjection;
using Sxm.MongoDB.Extensions;
using Sxm.MongoDB.Repositories;
using Sxm.MongoDB.Repositories.Collections.Users;

namespace Sxm.Core.Server;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, Action<CoreOptions>? options = null)
    {
        var opt = new CoreOptions();
        options?.Invoke(opt);
        
        var provider = new ServiceProvider(services);
        
        var commandManager = new CommandManager(provider);
        var commandProvider = new CommandProvider(commandManager);
        
        var settings = new MongoClientSettings
        {
#if DEBUG
            Server = new MongoServerAddress("localhost", 27017),
#endif
            ConnectTimeout = TimeSpan.FromSeconds(5),
            DirectConnection = true
        };
        
        services.UseMongoDb(settings);
        
        var db = new SxmDb(settings);
        var userRepository = new UserRepository(db);
        
        services.AddSingleton(typeof(SxmDb), typeof(SxmDb), db);
        services.AddSingleton(typeof(UserRepository), typeof(UserRepository), userRepository);
        
        services
            .AddSingleton<ICommandManager, CommandManager>(commandManager)
            .AddSingleton<ICommandProvider, CommandProvider>(commandProvider);
        
        foreach (var a in opt.Assemblies)
        {
            var types = a.GetExportedTypes();
        
            var entryType = types.FirstOrDefault(x => typeof(Script).IsAssignableFrom(x));
            if (entryType is null) continue;
        
            var instance = Activator.CreateInstance(entryType) as Script;
            instance?.OnConfigure(services);
        }
        
        commandManager.Initialize(opt.Assemblies.ToArray());
        return services;
    }
}