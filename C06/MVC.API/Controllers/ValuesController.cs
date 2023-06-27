using Microsoft.AspNetCore.Mvc;

namespace MVC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet("IActionResult")]
    [ProducesResponseType(typeof(Model), StatusCodes.Status200OK)]
    public IActionResult InterfaceAction()
        => Ok(new Model(nameof(InterfaceAction)));

    [HttpGet("ActionResult")]
    [ProducesResponseType(typeof(Model), StatusCodes.Status200OK)]
    public ActionResult ClassAction()
        => Ok(new Model(nameof(ClassAction)));

    [HttpGet("DirectModel")]
    public Model DirectModel()
        => new Model(nameof(DirectModel));

    [HttpGet("ActionResultT")]
    public ActionResult<Model> ActionResultT()
        => new Model(nameof(ActionResultT));

    [HttpGet("MultipleResults")]
    public ActionResult<Model> MultipleResults()
    {
        var condition = Random.Shared
            .GetItems(new[] { true, false }, 1)
            .First();
        return condition
            ? Ok(new Model(nameof(MultipleResults)))
            : NotFound();
    }

    public record class Model(string Name);
}
