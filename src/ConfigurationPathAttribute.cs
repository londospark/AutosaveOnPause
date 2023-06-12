using System;

namespace AutosaveOnPause;

[AttributeUsage(AttributeTargets.Class)]
public class ConfigurationPathAttribute : Attribute
{
    public ConfigurationPathAttribute(string value) => Value = value;

    public string Value { get; }
}