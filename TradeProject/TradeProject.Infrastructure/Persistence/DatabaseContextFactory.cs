using Microsoft.EntityFrameworkCore;

namespace TradeProject.Infrastructure.Persistence;

public class DatabaseContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureContext;

    public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureContext = configureDbContext;
    }

    public DatabaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
        _configureContext(optionsBuilder);

        return new DatabaseContext(optionsBuilder.Options);
    }

}
