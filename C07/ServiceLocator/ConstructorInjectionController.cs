using Microsoft.AspNetCore.Mvc;

namespace ServiceLocator;

public class ConstructorInjectionController : ControllerBase
{
    private readonly IMyService _myService;

    public ConstructorInjectionController(IMyService myService)
    {
        _myService = myService ?? throw new ArgumentNullException(nameof(myService));
    }

    [Route("/constructor-injection")]
    public IActionResult GetUsingConstructorInjection()
    {
        _myService.Execute();
        return Ok("Success!");
    }
}
