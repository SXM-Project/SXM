using System;
using System.Linq;
using Sxm.Core.Models;
using Sxm.Core.Services;
using Sxm.DependencyInjection;
using Sxm.DependencyInjection.Extensions;
using Sxm.DependencyInjection.Services;
using IServiceProvider = Sxm.DependencyInjection.IServiceProvider;

namespace Sxm.Core.Client.Services;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, Action<CoreOptions>? options = null)
    {
        var opt = new CoreOptions();
        options?.Invoke(opt);
        
        var provider = new ServiceProvider(services);
        services.AddSingleton<IServiceProvider, ServiceProvider>(provider);
        
        var commandManager = new CommandManager(provider);
        var commandProvider = new CommandProvider(commandManager);

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