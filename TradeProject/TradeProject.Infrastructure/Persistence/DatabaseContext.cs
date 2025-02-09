using Microsoft.EntityFrameworkCore;
using TradeProject.Domain;

namespace TradeProject.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AccountEntity> Account { get; set; }
    public DbSet<TradeEntity> Trade { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // configure account
        modelBuilder.Entity<AccountEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id)
                  .HasColumnType("uuid")
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.HasMany(e => e.Trades)
                  .WithOne(e => e.Account)
                  .HasForeignKey(e => e.AccountId);
        });

        // configure trade
        modelBuilder.Entity<TradeEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .HasColumnType("uuid")
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Type).IsRequired().HasConversion<string>();
            entity.Property(e => e.Status).IsRequired().HasConversion<string>();
            entity.Property(e => e.SecurityCode).HasMaxLength(3);
        });
    }
}