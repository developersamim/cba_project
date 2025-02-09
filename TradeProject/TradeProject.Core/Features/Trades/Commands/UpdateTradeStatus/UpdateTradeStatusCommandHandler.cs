using MediatR;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Core.Exceptions;
using TradeProject.Domain;

namespace TradeProject.Core.Features.Trades.Commands.UpdateTradeStatus;

public class UpdateTradeStatusCommandHandler : IRequestHandler<UpdateTradeStatusCommand>
{
    private readonly ITradeRepository _tradeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTradeStatusCommandHandler(ITradeRepository tradeRepository, IUnitOfWork unitOfWork)
    {
        _tradeRepository = tradeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateTradeStatusCommand request, CancellationToken cancellationToken)
    {
        var trade = await _tradeRepository.GetByIdAsync(request.Id);
        if (trade == null)
            throw new TradeNotFoundException(request.Id);

        if (!Enum.TryParse<TradeStatus>(request.Status, true, out var tradeStatus))
            throw new TradeStatusArgumentException(request.Status);

        trade.Status = tradeStatus;
        trade.Timestamp = DateTime.UtcNow;

        await _tradeRepository.UpdateAsync(trade);
        await _unitOfWork.SaveChangesAsync();
    }
}
