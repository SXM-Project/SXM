using CitizenFX.Core;

namespace Sxm.DependencyInjection;

public sealed class ServiceProvider(IServiceCollection services) : IServiceProvider
{
    public IReadOnlyList<object> GetServices() =>
        services.GetServices().Select(x => x.Instance).ToList();

    public object? GetService(Type serviceType)
    {
        var service = services.GetServices().FirstOrDefault(x => x.ServiceType == serviceType);
        Debug.WriteLine("Get: " + string.Join(", ", serviceType.Name, service?.Instance.GetType().Name, service?.Instance));
        return service?.Instance;
    }

    public T? GetService<T>() => (T?)GetService(typeof(T));
}