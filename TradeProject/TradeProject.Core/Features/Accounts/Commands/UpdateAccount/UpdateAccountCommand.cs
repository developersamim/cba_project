using MediatR;

namespace TradeProject.Core.Features.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommand : IRequest
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}