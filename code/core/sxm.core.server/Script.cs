using Sxm.DependencyInjection;

namespace Sxm.Core.Server;

public abstract class Script
{
    public abstract void OnConfigure(IServiceCollection services);
}