using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Domain;

namespace TradeProject.Core.Features.Accounts.Commands.CreateAccount;
public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountEntity = new AccountEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _accountRepository.AddAsync(accountEntity);
        await _unitOfWork.SaveChangesAsync();

        return accountEntity.Id;
    }
}