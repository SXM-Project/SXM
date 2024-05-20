using CitizenFX.Core;
using Sxm.Core.Client;
using Sxm.DependencyInjection;

namespace Sxm.Test.Client;

public class Main : Script
{
    public override void OnConfigure(IServiceCollection services)
    {
        services.AddSingleton<IPing, Ping>();
    }
}