using System;

namespace Sxm.Core.Descriptors;

public readonly struct CommandDescriptor(string name, Delegate action)
{
    public string Name => name;
    public Delegate Action => action;
}