using TradeProject.Core.Contracts.Persistence;
using TradeProject.Domain;
using TradeProject.Infrastructure.Persistence;

namespace TradeProject.Infrastructure.Repositories;

public class TradeRepository : BaseRepository<TradeEntity>, ITradeRepository
{
    public TradeRepository(DatabaseContext context)
        : base(context)
    {

    }
}