using CitizenFX.Core;
using Sxm.Core.Client;
using Sxm.DependencyInjection;

namespace Sxm.Client;

public sealed class Main : BaseScript
{
    public Main()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton(typeof(ExportDictionary), typeof(ExportDictionary), Exports);
        services.AddSingleton(typeof(EventHandlerDictionary), typeof(EventHandlerDictionary), EventHandlers);
        services.AddSingleton(typeof(StateBag), typeof(StateBag), GlobalState);
        services.AddSingleton(typeof(Player), typeof(Player), LocalPlayer);
        
        services.AddCore(options =>
        {
            options.Assemblies =
            [
                typeof(Main).Assembly,
                typeof(Sxm.Test.Client.Main).Assembly
            ];
        });
    }
}