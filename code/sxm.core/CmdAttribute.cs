using System;

namespace Sxm.Core;

[AttributeUsage(AttributeTargets.Method)]
public class CmdAttribute(string name) : Attribute
{
    public string Name => name;
}