using Microsoft.Extensions.Options;

namespace CommonScenarios;

public class MyNameServiceUsingNamedOptionsFactory : IMyNameService
{
    private readonly MyOptions _options1;
    private readonly MyOptions _options2;

    public MyNameServiceUsingNamedOptionsFactory(IOptionsFactory<MyOptions> myOptions)
    {
        _options1 = myOptions.Create("Options1");
        _options2 = myOptions.Create("Options2");
    }

    public string? GetName(bool someCondition)
    {
        return someCondition ? _options1.Name : _options2.Name;
    }
}
