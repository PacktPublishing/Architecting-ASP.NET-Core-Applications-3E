using Microsoft.Extensions.Options;

namespace ConfigurationGenerators;

[OptionsValidator]
public partial class MyOptionsValidator : IValidateOptions<MyOptions>
{
}