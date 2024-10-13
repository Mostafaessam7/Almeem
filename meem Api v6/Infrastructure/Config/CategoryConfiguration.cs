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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Key and properties configuration
            builder.Property(c => c.NameEn)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.NameAr)
                .HasMaxLength(100)
                .IsRequired();

            // Seed data
            builder.HasData(
                new Category { Id = 1, NameEn = "Blouse", NameAr = "بلوزة" },
                new Category { Id = 2, NameEn = "Cardigan", NameAr = "كارديجان" },
                new Category { Id = 3, NameEn = "Dresses", NameAr = "فساتين" },
                new Category { Id = 4, NameEn = "Skirts", NameAr = "تنانير" },
                new Category { Id = 5, NameEn = "Sale", NameAr = "تخفيضات" }
            );
        }
    }
}
