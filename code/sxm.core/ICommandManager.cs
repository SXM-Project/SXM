using System.Collections.Generic;

namespace Sxm.Core;

public interface ICommandManager
{
    IReadOnlyList<CommandDescriptor> Commands { get; }
}