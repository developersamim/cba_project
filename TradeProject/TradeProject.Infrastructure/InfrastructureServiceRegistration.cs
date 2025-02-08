using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeProject.Infrastructure.Persistence;

namespace TradeProject.Infrastructure;
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var a = configuration.GetConnectionString("DefaultConnection");
        Action<DbContextOptionsBuilder> configureDbContext = (o =>
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddDbContext<DatabaseContext>(configureDbContext);
        services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

        // Create database and tables from code
        var dataContext = services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
        dataContext.Database.EnsureCreated();

        return services;
    }
}