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
    public class CommentConfiguration: IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Content).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(x => x.ParentComment)
                   .WithMany(c => c.CommentsChild)
                   .HasForeignKey(x => x.ParentCommentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
