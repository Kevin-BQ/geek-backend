using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NameProduct).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.LargeDescription).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(x => x.Stock).IsRequired(false);
            builder.Property(x => x.Discount).IsRequired(false).HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.Brand).WithMany()
                   .HasForeignKey(x => x.BrandId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Category).WithMany()
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Subcategory).WithMany()
                   .HasForeignKey(x => x.SubCategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Images)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
