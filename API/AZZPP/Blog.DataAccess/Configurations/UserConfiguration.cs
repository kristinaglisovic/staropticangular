using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Configurations
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureTableSpecifics(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            //zbog hasha
            builder.Property(x => x.Password).IsRequired().HasMaxLength(230);

            builder.HasIndex(x => x.FirstName);
            builder.HasIndex(x => x.LastName);
            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Password).IsUnique();

            builder.HasMany(x => x.Posts)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Likes)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Comments)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //uloga a - slucaj koriscenja a, b, c
            builder.HasMany(x => x.UseCases).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }

    }
}
