using Sxm.DependencyInjection.Descriptors;

namespace Sxm.DependencyInjection;

public interface IServiceCollection : IList<ServiceDescriptor>
{
    IReadOnlyList<ServiceDescriptor> GetServices();
}