using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Models;

namespace TestTask.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.OrderId).IsRequired();
        builder.HasIndex(x => x.OrderId).IsUnique();
        builder.HasKey(x => x.OrderId);
        builder.Property(x => x.OrderDateTime).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ProductId);
    }
}