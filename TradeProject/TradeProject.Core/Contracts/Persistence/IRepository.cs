namespace TradeProject.Core.Contracts.Persistence
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> SearchAsync(IQueryable<T> query);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        IQueryable<T> GetQueryable();
    }
}