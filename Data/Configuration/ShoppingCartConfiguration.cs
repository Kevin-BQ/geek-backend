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
    public class ShoppingCartConfiguration: IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Discount).IsRequired(false).HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.User).WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
