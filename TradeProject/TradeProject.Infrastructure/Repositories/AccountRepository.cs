using TradeProject.Core.Contracts.Persistence;
using TradeProject.Domain;
using TradeProject.Infrastructure.Persistence;

namespace TradeProject.Infrastructure.Repositories;
public class AccountRepository : BaseRepository<AccountEntity>, IAccountRepository
{
    public AccountRepository(DatabaseContext context)
        : base(context) { }
}