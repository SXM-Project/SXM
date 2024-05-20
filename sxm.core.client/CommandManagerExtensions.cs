using System.Linq;

namespace Sxm.Core.Client;

public static class CommandManagerExtensions
{
    public static CommandDescriptor? GetCommand(this ICommandManager commandManager, string name) =>
        commandManager.Commands.FirstOrDefault(x => x.Name == name);
}