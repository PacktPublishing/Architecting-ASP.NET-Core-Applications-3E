using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalApp.Features.Products;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListAllProducts.Result>>> GetAsync()
    {
        var result = await _mediator.Send(new ListAllProducts.Query());
        return Ok(result);
    }
}
