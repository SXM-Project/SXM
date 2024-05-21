using Sxm.Core.Client;
using Sxm.DependencyInjection;
using Sxm.DependencyInjection.Extensions;

namespace Sxm.Economy.Client;

public class Main : Script
{
    public override void OnConfigure(IServiceCollection services)
    {
        services.AddSingleton<IPing, Ping>();
    }
}