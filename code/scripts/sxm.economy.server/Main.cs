using Sxm.Core.Server;
using Sxm.DependencyInjection;
using Sxm.DependencyInjection.Extensions;

namespace Sxm.Economy.Server;

public class Main : Script
{
    public override void OnConfigure(IServiceCollection services)
    {
        services.AddSingleton<IPing, Ping>();
    }
}