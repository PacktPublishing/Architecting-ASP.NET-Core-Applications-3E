namespace OptionsConfiguration;

public class ConfigureMeOptions
{
    public string? Title { get; set; }
    public IEnumerable<string> Lines { get; set; } = Enumerable.Empty<string>();
}
