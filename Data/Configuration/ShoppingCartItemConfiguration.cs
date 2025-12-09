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
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.ShoppingCart).WithMany()
                   .HasForeignKey(x => x.CartId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product).WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
