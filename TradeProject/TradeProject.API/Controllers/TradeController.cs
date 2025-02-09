using MediatR;
using Microsoft.AspNetCore.Mvc;
using TradeProject.Core.Exceptions;
using TradeProject.Core.Features.Trades.Commands.CreateTrade;
using TradeProject.Core.Features.Trades.Commands.UpdateTradeStatus;

namespace TradeProject.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TradeController : ControllerBase
{
    private readonly IMediator _mediator;

    public TradeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrade([FromBody] CreateTradeCommand request)
    {
        try
        {
            var id = await _mediator.Send(request);

            return Ok(id);
        }
        catch (TradeTypeArgumentException)
        {
            return BadRequest($"Trade type {request.Type} could not be found");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTradeStatusCommand request)
    {
        if (id == Guid.Empty || id != request.Id)
            return BadRequest();

        try
        {
            await _mediator.Send(request);

            return NoContent();
        }
        catch (TradeNotFoundException)
        {
            return NotFound();
        }
        catch (TradeStatusArgumentException)
        {
            return BadRequest($"Trade with status {request.Status} not found.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
