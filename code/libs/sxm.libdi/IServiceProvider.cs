namespace Sxm.DependencyInjection;

public interface IServiceProvider
{
    IReadOnlyList<object> GetServices();
    object? GetService(Type serviceType);
    TService? GetService<TService>();
}