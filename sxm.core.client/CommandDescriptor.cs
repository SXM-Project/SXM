using System;
using System.Linq;

namespace Sxm.Core.Client;

public readonly struct CommandDescriptor(string name, Delegate action)
{
    public string Name => name;
    public Delegate Action => action;
}