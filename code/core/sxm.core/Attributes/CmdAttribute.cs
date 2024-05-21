using System;

namespace Sxm.Core.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class CmdAttribute(string name) : Attribute
{
    public string Name => name;
}