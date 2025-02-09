using MediatR;
using Microsoft.AspNetCore.Mvc;
using TradeProject.Core.Exceptions;
using TradeProject.Core.Features.Accounts.Commands.CreateAccount;
using TradeProject.Core.Features.Accounts.Queries.GetAccountById;
using TradeProject.Core.Features.Accounts.Queries.GetAccounts;

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

    [HttpGet]
    public async Task<IActionResult> GetAccounts()
    {
        var query = new GetAccountsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
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
            await _mediator.Send(request);

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}