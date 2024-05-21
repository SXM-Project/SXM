using System.Linq.Expressions;
using System.Reflection;
using CitizenFX.Core;
using Sxm.Core.Attributes;
using Sxm.Core.Descriptors;
using IServiceProvider = Sxm.DependencyInjection.IServiceProvider;

namespace Sxm.Core.Server.Services;

internal class ExportManager(IServiceProvider services, ExportDictionary exports) : IExportManager, IInitializable
{
    private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                       BindingFlags.Instance | BindingFlags.FlattenHierarchy;

    public void Initialize(Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetExportedTypes();

            foreach (var type in types)
            {
                if (type == GetType()) continue;
             
                Debug.WriteLine("Exporting: " + type);
                
                var service = services.GetService(type);
                if (service is null) continue;
                
                // Get methods from the implementation type instead of the service type
                var methods = service.GetType().GetMethods(Flags);

                foreach (var method in methods)
                {
                    var attr = method.GetCustomAttribute<ExportAttribute>();
                    if (attr is null) continue;

                    AddExport(attr, service, method);
                }
            }
        }
    }

    private void RegisterExport(ExportDescriptor export)
    {
        exports.Add(export.Name, export.Action);
        Debug.WriteLine($"Registering export: {export.Name}.");
    }

    private void AddExport(ExportAttribute attr, object instance, MethodInfo method)
    {
        var action =
            Delegate.CreateDelegate(
                Expression.GetDelegateType((from parameter in method.GetParameters() select parameter.ParameterType)
                    .Concat(new[] { method.ReturnType }).ToArray()), instance, method);

        var funcName = attr.Name ?? method.Name;
        var export = new ExportDescriptor(funcName, action);

        RegisterExport(export);
    }
}