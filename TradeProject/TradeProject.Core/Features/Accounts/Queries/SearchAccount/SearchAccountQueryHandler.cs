using AutoMapper;
using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.DTO;
using TradeProject.Domain;

namespace TradeProject.Core.Features.Accounts.Queries.SearchAccount;

public class SearchAccountQueryHandler : IRequestHandler<SearchAccountQuery, IEnumerable<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public SearchAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountDto>> Handle(SearchAccountQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AccountEntity> query = _accountRepository.GetQueryable();

        if (request.Id != Guid.Empty)
            query = query.Where(a => a.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.FirstName))
            query = query.Where(a => a.FirstName == request.FirstName);

        if (!string.IsNullOrWhiteSpace(request.LastName))
            query = query.Where(a => a.LastName == request.LastName);

        var accounts = await _accountRepository.SearchAsync(query);

        var response = _mapper.Map<IEnumerable<AccountDto>>(accounts);

        return response;
    }
}
