using Microsoft.Extensions.Options;

namespace CommonScenarios;

public class MyNameServiceUsingNamedOptionsSnapshot : IMyNameService
{
    private readonly MyOptions _options1;
    private readonly MyOptions _options2;

    public MyNameServiceUsingNamedOptionsSnapshot(IOptionsSnapshot<MyOptions> myOptions)
    {
        _options1 = myOptions.Get("Options1");
        _options2 = myOptions.Get("Options2");
    }

    public string? GetName(bool someCondition)
    {
        return someCondition ? _options1.Name : _options2.Name;
    }
}
