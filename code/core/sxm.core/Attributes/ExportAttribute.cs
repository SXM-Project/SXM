using System;

namespace Sxm.Core.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ExportAttribute(string? name = null) : Attribute
{
    public string? Name { get; internal set; } = name;
}