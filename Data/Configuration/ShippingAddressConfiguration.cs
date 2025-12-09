using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configuration
{
    public class ShippingAddressConfiguration: IEntityTypeConfiguration<ShippingAddress>
    {
        public void Configure(EntityTypeBuilder<ShippingAddress> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
            builder.Property(x => x.City).IsRequired().HasMaxLength(50);
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.Country).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ZipCode).IsRequired();

            builder.HasOne(x => x.User)
               .WithMany(u => u.ShippingAddresses)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
