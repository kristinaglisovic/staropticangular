using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class TagConfiguration : EntityConfiguration<Tag>
    {

        protected override void ConfigureTableSpecifics(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.Name).IsUnique();


            builder.HasMany(x => x.Posts)
                   .WithOne(x => x.Tag)
                   .HasForeignKey(x => x.TagId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TagTranslation).WithOne(x => x.Tag).HasForeignKey<TagTranslation>(x => x.TagId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
