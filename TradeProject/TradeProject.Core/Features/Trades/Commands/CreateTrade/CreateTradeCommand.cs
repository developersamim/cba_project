using System.ComponentModel.DataAnnotations;
using MediatR;
using TradeProject.Domain;

namespace TradeProject.Core.Features.Trades.Commands.CreateTrade;

public class CreateTradeCommand : IRequest<Guid>
{
    public Guid AccountId { get; set; }
    [StringLength(3, MinimumLength = 3)]
    public string SecurityCode { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
}
