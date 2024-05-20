using CitizenFX.Core;
using Sxm.Core.Server;
using Sxm.DependencyInjection;

namespace Sxm.Server;

public sealed class Main : BaseScript
{
    public Main()
    {
        var services = new ServiceCollection();
        
        services.AddCore(options =>
        {
            options.Assemblies =
            [
                typeof(Main).Assembly,
                typeof(Sxm.Test.Server.Main).Assembly
            ];
        });
    }
}