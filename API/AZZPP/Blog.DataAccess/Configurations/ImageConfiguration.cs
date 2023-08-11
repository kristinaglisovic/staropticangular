using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Configurations
{
    public class ImageConfiguration : EntityConfiguration<Image>
    {
        protected override void ConfigureTableSpecifics(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path).IsRequired(true).HasMaxLength(230);

            builder.HasIndex(x => x.Path);

            builder.HasMany(x => x.Posts)
                   .WithOne(x => x.Image)
                   .HasForeignKey(x => x.ImageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.User)
           .WithOne(i => i.Image)
           .HasForeignKey<User>(b => b.ImageId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
