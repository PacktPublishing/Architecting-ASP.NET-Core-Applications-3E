using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalApp.Features.Stocks;

[ApiController]
[Route("products/{productId}/")]
public class StocksController : ControllerBase
{
    private readonly IMediator _mediator;
    public StocksController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("add-stocks")]
    public async Task<ActionResult<AddStocks.Result>> AddAsync(
        int productId,
        [FromBody] AddStocks.Command command
    )
    {
        try
        {
            command.ProductId = productId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(new
            {
                ex.Message,
                productId,
            });
        }
    }

    [HttpPost("remove-stocks")]
    public async Task<ActionResult<RemoveStocks.Result>> RemoveAsync(
        int productId,
        [FromBody] RemoveStocks.Command command
    )
    {
        try
        {
            command.ProductId = productId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (NotEnoughStockException ex)
        {
            return Conflict(new
            {
                ex.Message,
                ex.AmountToRemove,
                ex.QuantityInStock
            });
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(new
            {
                ex.Message,
                productId,
            });
        }
    }
}
