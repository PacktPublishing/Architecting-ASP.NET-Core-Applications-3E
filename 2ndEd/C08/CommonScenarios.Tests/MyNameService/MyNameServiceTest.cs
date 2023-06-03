using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CommonScenarios;

public abstract class MyNameServiceTest<TMyNameService>
    where TMyNameService : class, IMyNameService
{
    protected readonly IMyNameService _sut;

    public const string Option1Name = "Options 1";
    public const string Option2Name = "Options 2";

    public MyNameServiceTest()
    {
        var services = new ServiceCollection();
        services.AddTransient<IMyNameService, TMyNameService>();
        services.Configure<MyOptions>("Options1", myOptions =>
        {
            myOptions.Name = Option1Name;
        });
        services.Configure<MyOptions>("Options2", myOptions =>
        {
            myOptions.Name = Option2Name;
        });
        services.Configure<MyDoubleNameOptions>(options =>
        {
            options.FirstName = Option1Name;
            options.SecondName = Option2Name;
        });
        _sut = services.BuildServiceProvider().GetRequiredService<IMyNameService>();
    }

    [Fact]
    public void GetName_should_return_Name_from_options1_when_someCondition_is_true()
    {
        var result = _sut.GetName(true);
        Assert.Equal(Option1Name, result);
    }

    [Fact]
    public void GetName_should_return_Name_from_options2_when_someCondition_is_false()
    {
        var result = _sut.GetName(false);
        Assert.Equal(Option2Name, result);
    }
}
