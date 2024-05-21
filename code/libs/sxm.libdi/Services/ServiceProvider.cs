namespace Sxm.DependencyInjection.Services;

public sealed class ServiceProvider(IServiceCollection services) : IServiceProvider
{
    public IReadOnlyList<object> GetServices() =>
        services.GetServices().Select(x => x.Instance).ToList();

    public object? GetService(Type serviceType)
    {
        var service = services.GetServices().FirstOrDefault(x => x.ServiceType == serviceType);
        return service?.Instance;
    }

    public T? GetService<T>() => (T?)GetService(typeof(T));
}