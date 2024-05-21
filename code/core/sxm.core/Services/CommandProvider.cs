using System.Linq;
using Sxm.Core.Descriptors;

namespace Sxm.Core.Services;

public sealed class CommandProvider(ICommandManager commandManager) : ICommandProvider
{
    public CommandDescriptor? GetCommand(string name) => commandManager.Commands.FirstOrDefault(x => x.Name == name);
}