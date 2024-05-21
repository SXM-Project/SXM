using Sxm.Core.Server;
using Sxm.DependencyInjection;

namespace Sxm.Economy.Server;

public class Main : Script
{
    public override void OnConfigure(IServiceCollection services)
    {
        services.AddSingleton<IPing, Ping>();
    }
}