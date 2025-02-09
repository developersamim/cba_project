using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.Exceptions;

namespace TradeProject.Core.Features.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.Id);

        if (account == null)
            throw new AccountNotFoundException(request.Id);

        if (!string.IsNullOrWhiteSpace(request.FirstName))
            account.FirstName = request.FirstName;
        if (!string.IsNullOrWhiteSpace(request.LastName))
            account.LastName = request.LastName;

        await _accountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync();
    }
}
