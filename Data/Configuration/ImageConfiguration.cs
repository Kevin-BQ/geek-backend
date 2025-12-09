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
    public class ImageConfiguration:  IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UrlImage).IsRequired();

            builder.HasOne(x => x.Product).WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
