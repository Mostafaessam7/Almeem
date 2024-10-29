using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Key and properties configuration
        builder.Property(p => p.NameEn)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.NameAr)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.DescriptionEn)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(p => p.DescriptionAr)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        // Relationships
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
