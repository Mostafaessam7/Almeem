using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.Property(pv => pv.Size)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(pv => pv.Color)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(pv => pv.QuantityInStock)
                .IsRequired();

            // Relationships
            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure unique combination of Product, Size, and Color
            builder.HasIndex(pv => new { pv.ProductId, pv.Size, pv.Color })
                .IsUnique();
        }
    }
}
