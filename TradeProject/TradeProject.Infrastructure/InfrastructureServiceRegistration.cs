using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeProject.Core.Contracts.Persistence;
using TradeProject.Infrastructure.Persistence;
using TradeProject.Infrastructure.Repositories;

namespace TradeProject.Infrastructure;
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var a = configuration.GetConnectionString("DefaultConnection");
        Action<DbContextOptionsBuilder> configureDbContext = (o =>
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddDbContext<DatabaseContext>(configureDbContext);
        //services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork<DatabaseContext>>();

        // Create database and tables from code
        var dataContext = services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
        dataContext.Database.EnsureCreated();

        return services;
    }
}