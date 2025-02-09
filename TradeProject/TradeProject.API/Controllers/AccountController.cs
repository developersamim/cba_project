using MediatR;
using Microsoft.AspNetCore.Mvc;
using TradeProject.Core.Exceptions;
using TradeProject.Core.Features.Accounts.Commands.CreateAccount;
using TradeProject.Core.Features.Accounts.Commands.DeleteAccount;
using TradeProject.Core.Features.Accounts.Commands.UpdateAccount;
using TradeProject.Core.Features.Accounts.Queries.GetAccountById;
using TradeProject.Core.Features.Accounts.Queries.GetAccounts;
using TradeProject.Core.Features.Accounts.Queries.SearchAccount;

namespace TradeProject.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAccounts()
    {
        try
        {
            var query = new GetAccountsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid id provided");

        try
        {
            var query = new GetAccountByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (AccountNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var id = await _mediator.Send(request);

            return Ok(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAccountCommand request)
    {
        if (id == Guid.Empty || id != request.Id)
            return BadRequest();

        try
        {
            await _mediator.Send(request);

            return NoContent();
        }
        catch (AccountNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        try
        {
            var command = new DeleteAccountCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
        catch (AccountNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchAccountQuery request)
    {
        try
        {
            var accounts = await _mediator.Send(request);

            return Ok(accounts);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}