using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Sxm.Core.Attributes;
using Sxm.Core.Descriptors;
using IServiceProvider = Sxm.DependencyInjection.IServiceProvider;

namespace Sxm.Core.Client;

internal class CommandManager(IServiceProvider services) : ICommandManager, IInitializable
{
    private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                       BindingFlags.Instance | BindingFlags.FlattenHierarchy;

    private readonly List<CommandDescriptor> _commands = [];

    public IReadOnlyList<CommandDescriptor> Commands => _commands;
    
    public void Initialize(Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetExportedTypes();
            
            foreach (var type in types)
            {
                if (type == GetType()) continue;
                
                var service = services.GetService(type);
                if (service is null) continue;

                // Get methods from the implementation type instead of the service type
                var methods = service.GetType().GetMethods(Flags);

                foreach (var method in methods)
                {
                    var attr = method.GetCustomAttribute<CmdAttribute>();
                    if (attr is null) continue;
                    
                    AddCommand(attr, service, method);
                }
            }
        }
    }

    private void RegisterCommand(CommandDescriptor command)
    {
        API.RegisterCommand(command.Name, new Action<int, List<object>, string>(async (source, args, raw) =>
        {
            var newArgs = new List<object>();
            var methodParams = command.Action.Method.GetParameters();

            if (args.Count == methodParams.Length)
            {
                try
                {
                    args.ForEach(x =>
                        newArgs.Add(Convert.ChangeType(x, methodParams[args.FindIndex(p => p == x)].ParameterType)));
                    
                    command.Action.DynamicInvoke(newArgs.ToArray());
                }
                catch
                {
                    Debug.WriteLine($"Unable to convert client command arguments.");
                }
            }
            else
            {
                var usage = "";
                methodParams.ToList().ForEach(x => usage += $"<{x.Name}_{x.ParameterType.Name.ToLower()}> ");
                
                Debug.WriteLine($"Invalid client command usage: {command.Name} {usage}.");
            }
        }), false);

        Debug.WriteLine($"Registering command: {command.Name}.");
    }

    private void AddCommand(CmdAttribute attr, object instance, MethodInfo method)
    {
        var action =
            Delegate.CreateDelegate(
                Expression.GetDelegateType((from parameter in method.GetParameters() select parameter.ParameterType)
                    .Concat(new[] { method.ReturnType }).ToArray()), instance, method);

        var command = new CommandDescriptor(attr.Name, action);
        _commands.Add(command);
        
        RegisterCommand(command);
    }
}