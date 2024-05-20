using System;
using System.Collections.Generic;

namespace Sxm.Core.Client;

public interface ICommandManager
{
    internal IReadOnlyList<CommandDescriptor> Commands { get; }
}