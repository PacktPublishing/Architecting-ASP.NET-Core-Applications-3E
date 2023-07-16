using System.ComponentModel.DataAnnotations;

namespace ConfigurationGenerators;

public class MyOptions
{
    [Required]
    public string? Name { get; set; }
}
