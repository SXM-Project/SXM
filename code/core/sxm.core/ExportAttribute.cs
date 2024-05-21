﻿using System;

namespace Sxm.Core;

[AttributeUsage(AttributeTargets.Method)]
public class ExportAttribute(string? name = null) : Attribute
{
    public string? Name { get; internal set; } = name;
}