using Sxm.Core.Server;
using Sxm.DependencyInjection;

namespace Sxm.Test.Server;

public class Main : Script
{
    public override void OnConfigure(IServiceCollection services)
    {
        services.AddSingleton<IPing, Ping>();
    }
}