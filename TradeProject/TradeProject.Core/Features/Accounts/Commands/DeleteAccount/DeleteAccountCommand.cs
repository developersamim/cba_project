using MediatR;

namespace TradeProject.Core.Features.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommand : IRequest
{
    public Guid Id { get; set; }
}
