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
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.Property(x => x.Id).IsRequired();


            builder.HasOne(x => x.UserAplication).WithMany()
                   .HasForeignKey(x => x.UserAplicationId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product).WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
