using System;

namespace Sxm.Core.Client;

[AttributeUsage(AttributeTargets.Method)]
public class CmdAttribute(string name) : Attribute
{
    public string Name => name;
}