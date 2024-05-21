using System.Reflection;
using Sxm.DependencyInjection.Descriptors;

namespace Sxm.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                       BindingFlags.Instance | BindingFlags.FlattenHierarchy;

    private static object? CreateSingletonInstance(Type implementationType, params object[] args) =>
        Activator.CreateInstance(implementationType, args.ToArray());

    private static IReadOnlyList<object> GetServicesForConstructorType(IServiceCollection services,
        Type implementationType)
    {
        var ctor = implementationType.GetConstructors(Flags).FirstOrDefault();
        if (ctor is null) return [];

        var newArgs = new List<object>();
        
        foreach (var param in ctor.GetParameters())
        {
            var service = services.FirstOrDefault(x => x.ServiceType == param.ParameterType);
            if (service is null or { Instance: null }) continue;

            newArgs.Add(service.Instance);
        }

        return newArgs;
    }

    private static IServiceCollection Add(IServiceCollection services, Type serviceType, Type implementationType,
        ServiceLifetime lifetime, object? instance = null)
    {
        var args = GetServicesForConstructorType(services, implementationType);
        instance ??= CreateSingletonInstance(implementationType, args.ToArray());

        if (instance is null)
            throw new Exception($"Unable to create service: [{serviceType}].");

        var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime, instance);
        services.Add(descriptor);

        return services;
    }

    public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType,
        Type implementationType, object? instance = null) =>
        Add(services, serviceType, implementationType, ServiceLifetime.Singleton, instance);

    public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services,
        TImplementation? instance = default)
        where TService : class => AddSingleton(services, typeof(TService), typeof(TImplementation), instance);
}