using System.Linq;

namespace Sxm.Core.Client;

internal class CommandProvider(ICommandManager commandManager) : ICommandProvider
{
    public CommandDescriptor? GetCommand(string commandName) => commandManager.Commands.FirstOrDefault(x => x.Name == commandName);
}