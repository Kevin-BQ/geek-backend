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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.ListPrice).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(x => x.Discount).IsRequired().HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.Order).WithMany()
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product).WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
