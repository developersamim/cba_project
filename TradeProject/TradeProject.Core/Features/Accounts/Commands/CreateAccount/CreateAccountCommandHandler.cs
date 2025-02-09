using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Domain;

namespace TradeProject.Core.Features.Accounts.Commands.CreateAccount;
public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        throw new Exception();
        var accountEntity = new AccountEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _accountRepository.AddAsync(accountEntity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }

    Task IRequestHandler<CreateAccountCommand>.Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}