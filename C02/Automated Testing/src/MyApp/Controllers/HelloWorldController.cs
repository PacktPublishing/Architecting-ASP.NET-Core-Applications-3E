using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers;

[Route("")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public string Hello()
    {
        return "Hello World!";
    }
}
