using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Configurations;

namespace TestTask.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
    }
}