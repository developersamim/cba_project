using Microsoft.EntityFrameworkCore;
using TradeProject.Domain;

namespace TradeProject.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AccountEntity> Account { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // autogenerate account id
        modelBuilder.Entity<AccountEntity>()
            .Property(p => p.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();
    }
}