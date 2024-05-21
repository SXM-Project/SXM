using System.Linq;
using Sxm.Core.Descriptors;

namespace Sxm.Core.Extensions;

public static class CommandManagerExtensions
{
    public static CommandDescriptor? GetCommand(this ICommandManager commandManager, string name) =>
        commandManager.Commands.FirstOrDefault(x => x.Name == name);
}