using Sxm.DependencyInjection;

namespace Sxm.Core.Client;

public abstract class Script
{
    public abstract void OnConfigure(IServiceCollection services);
}