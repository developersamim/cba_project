using MediatR;
using TradeProject.Core.DTO;
using TradeProject.Core.Features.Accounts.Queries.GetAccounts;

namespace TradeProject.Core.Features.Accounts.Queries;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
    public GetAccountsQueryHandler()
    {

    }

    public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return new List<AccountDto>
        {
            new AccountDto
            {
                Id = Guid.NewGuid(),
                FirstName = "samim",
                LastName = "ahmed"
            }
        };
    }
}