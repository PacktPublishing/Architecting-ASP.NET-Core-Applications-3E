
Console.WriteLine(ValueOrDefault(default, "Default value"));
Console.WriteLine(ValueOrDefault("Some value", "Default value"));

Console.WriteLine(ValueOrDefaultPlain(default, "Default value"));
Console.WriteLine(ValueOrDefaultPlain("Some value", "Default value"));

static string ValueOrDefault(string? value, string defaultValue)
{
    return value ?? defaultValue;
}

static string ValueOrDefaultPlain(string? value, string defaultValue)
{
    if (value == null)
    {
        return defaultValue;
    }
    return value;
}
