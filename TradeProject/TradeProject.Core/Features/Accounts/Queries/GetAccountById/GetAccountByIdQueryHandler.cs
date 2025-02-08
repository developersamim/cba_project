using MediatR;
using TradeProject.Core.DTO;

namespace TradeProject.Core.Features.Accounts.Queries.GetAccountById
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
    {
        public GetAccountByIdQueryHandler()
        {

        }

        public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {

            return new AccountDto
            {
                Id = Guid.NewGuid(),
                FirstName = "heeeroine",
                LastName = "angry girl"
            };
        }
    }
}