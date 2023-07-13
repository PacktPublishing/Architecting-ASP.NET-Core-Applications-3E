using Microsoft.AspNetCore.Mvc;

namespace ServiceLocator;

public class MyController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    public MyController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    [Route("/service-locator")]
    public IActionResult Get()
    {
        using var myService = _serviceProvider.GetRequiredService<IMyService>(); // Don't
        myService.Execute();
        return Ok("Success!");
    }
}
