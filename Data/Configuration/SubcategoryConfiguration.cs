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
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NameSubcategory).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();


            builder.HasOne(x => x.Category).WithMany()
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
