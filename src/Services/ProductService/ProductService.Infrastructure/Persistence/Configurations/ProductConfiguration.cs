using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Persistence.Configurations
{
    public sealed class ProductConfiguration
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(
            EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            builder.Property(x => x.Price)
                .HasColumnName("price")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.StockQuantity)
                .HasColumnName("stock_quantity")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
