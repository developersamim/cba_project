using Microsoft.EntityFrameworkCore;
using TradeProject.Core.Contracts.Persistence;

namespace TradeProject.Infrastructure.Repositories;
public class UnitOfWork<T> : IUnitOfWork where T : DbContext
{
    private readonly T _dbContext;

    public UnitOfWork(T dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}