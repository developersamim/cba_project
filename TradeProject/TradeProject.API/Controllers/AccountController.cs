using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        var query = new GetAccountByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}