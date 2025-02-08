using MediatR;
using TradeProject.Core.DTO;

namespace TradeProject.Core.Features.Accounts.Queries.GetAccountById;

public class GetAccountByIdQuery : IRequest<AccountDto>
{
    public Guid Id { get; set; }
}