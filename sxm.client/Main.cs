using CitizenFX.Core;
using Sxm.Core.Client;
using Sxm.DependencyInjection;

namespace Sxm.Client;

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
                typeof(Test.Client.Main).Assembly
            ];
        });
    }
}