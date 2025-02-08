using MediatR;
using TradeProject.Core.DTO;

namespace TradeProject.Core.Features.Accounts.Queries.GetAccounts
{
    public class GetAccountsQuery : IRequest<IEnumerable<AccountDto>>
    {

    }
}