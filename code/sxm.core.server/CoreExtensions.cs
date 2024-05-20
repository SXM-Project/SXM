using Microsoft.Extensions.DependencyInjection;
using Sxm.Core.Client;

namespace Sxm.Core.Server;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, Action<CoreOptions>? options = null)
    {
        var opt = new CoreOptions();
        options?.Invoke(opt);

        var provider = services.BuildServiceProvider();

        var commandManager = new CommandManager(provider);
        var commandProvider = new CommandProvider(commandManager);

        services
            .AddSingleton<ICommandManager>(commandManager)
            .AddSingleton<ICommandProvider>(commandProvider);

        foreach (var a in opt.Assemblies)
        {
            var types = a.GetExportedTypes();

            var entryType = types.FirstOrDefault(x => x.IsAssignableTo(typeof(Script)));
            if (entryType is null) continue;

            var instance = Activator.CreateInstance(entryType) as Script;
            instance?.OnConfigure(services);
        }

        commandManager.Initialize(opt.Assemblies.ToArray());
        return services;
    }
}