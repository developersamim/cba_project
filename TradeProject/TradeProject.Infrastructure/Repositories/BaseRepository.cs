
using Microsoft.EntityFrameworkCore;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Infrastructure.Persistence;

namespace TradeProject.Infrastructure.Repositories;
public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly DatabaseContext _context;

    public BaseRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> SearchAsync(IQueryable<T> query)
    {
        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }

    public IQueryable<T> GetQueryable()
    {
        return _context.Set<T>();
    }
}