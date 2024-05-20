namespace Sxm.DependencyInjection;

public sealed class ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime, object instance)
{
    public Type ServiceType => serviceType;
    public Type ImplementationType => implementationType;
    public ServiceLifetime Lifetime => lifetime;
    public object Instance => instance;
}