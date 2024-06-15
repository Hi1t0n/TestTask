using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Domain.Interfaces;
using TestTask.Infrastructure.Context;
using TestTask.Infrastructure.Managers;

namespace TestTask.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,
        IConfiguration configuration, string connectionString)
    {
        serviceCollection.AddDataBase(connectionString);
        serviceCollection.AddManager();
        return serviceCollection;
    }

    private static IServiceCollection AddManager(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductManager, ProductManager>();
        serviceCollection.AddScoped<IOrderManager, OrderManager>();
        return serviceCollection;
    }

    private static IServiceCollection AddDataBase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(builder => builder.UseSqlite(connectionString));
        return serviceCollection;
    }
}