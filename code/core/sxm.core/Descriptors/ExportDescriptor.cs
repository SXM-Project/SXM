using System;

namespace Sxm.Core.Descriptors;

public readonly struct ExportDescriptor(string name, Delegate action)
{
    public string Name => name;
    public Delegate Action => action;
}