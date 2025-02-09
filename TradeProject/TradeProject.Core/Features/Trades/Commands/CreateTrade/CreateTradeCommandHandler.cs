using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.Exceptions;
using TradeProject.Domain;

namespace TradeProject.Core.Features.Trades.Commands.CreateTrade;

public class CreateTradeCommandHandler : IRequestHandler<CreateTradeCommand, Guid>
{
    private readonly ITradeRepository _tradeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTradeCommandHandler(ITradeRepository tradeRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _tradeRepository = tradeRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateTradeCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
            throw new AccountNotFoundException(request.AccountId);

        if (!Enum.TryParse<TradeType>(request.Type, true, out var tradeType))
            throw new TradeTypeArgumentException(request.Type);


        var tradeEntity = new TradeEntity
        {
            AccountId = request.AccountId,
            SecurityCode = request.SecurityCode,
            Amount = request.Amount,
            Type = tradeType
        };

        await _tradeRepository.AddAsync(tradeEntity);
        await _unitOfWork.SaveChangesAsync();

        return tradeEntity.Id;
    }
}
