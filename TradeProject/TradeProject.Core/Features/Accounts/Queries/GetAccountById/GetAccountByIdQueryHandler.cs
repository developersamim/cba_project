using AutoMapper;
using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.DTO;
using TradeProject.Core.Exceptions;

namespace TradeProject.Core.Features.Accounts.Queries.GetAccountById;
public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.Id);

        if (account == null)
            throw new AccountNotFoundException(request.Id);

        var accountDto = _mapper.Map<AccountDto>(account);

        return accountDto;
    }
}