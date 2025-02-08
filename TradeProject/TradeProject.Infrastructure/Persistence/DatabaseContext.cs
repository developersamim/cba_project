using Microsoft.EntityFrameworkCore;
using TradeProject.Domain;

namespace TradeProject.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AccountEntity> Account { get; set; }
}