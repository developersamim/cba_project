namespace TradeProject.Core.Contracts.Persistence;
public interface IUnitOfWork
{
    Task SaveChangesAsync();
}