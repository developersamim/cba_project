using MediatR;

namespace TradeProject.Core.Features.Trades.Commands.UpdateTradeStatus;

public class UpdateTradeStatusCommand : IRequest
{
    public Guid Id { get; set; }
    public string Status { get; set; }
}
