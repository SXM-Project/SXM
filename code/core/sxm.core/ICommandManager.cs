using System.Collections.Generic;
using Sxm.Core.Descriptors;

namespace Sxm.Core;

public interface ICommandManager
{
    IReadOnlyList<CommandDescriptor> Commands { get; }
}