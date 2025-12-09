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
    public class ShipmentConfiguration: IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.ShipmentDate).HasConversion(typeof(DateTime)).IsRequired();
            builder.Property(x => x.ShipmentDate).IsRequired();
            builder.Property(x => x.ShipmentMethod).IsRequired();

        }
    }
}
