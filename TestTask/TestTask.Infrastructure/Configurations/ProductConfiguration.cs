using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Models;

namespace TestTask.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.ProductId);
        builder.Property(x => x.ProductId).IsRequired();
        builder.HasIndex(x => x.ProductId).IsUnique();
        builder.Property(x => x.ProductDescription).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.ProductName).IsRequired().HasMaxLength(100);
    }
}