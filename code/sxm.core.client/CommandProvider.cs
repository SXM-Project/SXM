using System.Linq;

namespace Sxm.Core.Client;

internal class CommandProvider(ICommandManager commandManager) : ICommandProvider
{
    public CommandDescriptor? GetCommand(string name) => commandManager.Commands.FirstOrDefault(x => x.Name == name);
}