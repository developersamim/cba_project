using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.Exceptions;

namespace TradeProject.Core.Features.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.Id);

        if (account == null)
            throw new AccountNotFoundException(request.Id);

        await _accountRepository.DeleteAsync(account);
        await _unitOfWork.SaveChangesAsync();
    }
}
