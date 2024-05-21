using Sxm.Core.Client;
using Sxm.DependencyInjection;

namespace Sxm.Economy.Client;

public class Main : Script
{
    public override void OnConfigure(IServiceCollection services)
    {
        services.AddSingleton<IPing, Ping>();
    }
}