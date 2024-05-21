using System;

namespace Sxm.Core;

public readonly struct ExportDescriptor(string name, Delegate action)
{
    public string Name => name;
    public Delegate Action => action;
}