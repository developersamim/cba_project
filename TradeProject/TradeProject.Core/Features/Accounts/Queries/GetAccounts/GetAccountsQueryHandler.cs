using AutoMapper;
using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.DTO;
using TradeProject.Core.Features.Accounts.Queries.GetAccounts;

namespace TradeProject.Core.Features.Accounts.Queries;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetAllAsync();

        var result = _mapper.Map<IEnumerable<AccountDto>>(accounts);

        return result;
    }
}