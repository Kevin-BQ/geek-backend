using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.OrderDate).HasConversion(typeof(DateTime)).IsRequired();
            builder.Property(x => x.RequiredDate).HasConversion(typeof(DateTime)).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.OrderStatus).IsRequired(false);

            builder.HasOne(x => x.User).WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
