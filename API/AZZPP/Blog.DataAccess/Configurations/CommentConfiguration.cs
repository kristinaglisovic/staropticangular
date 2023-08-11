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
    public class CommentConfiguration : EntityConfiguration<Comment>
    {
        protected override void ConfigureTableSpecifics(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text).IsRequired().HasMaxLength(150);

            builder.HasIndex(x => x.Text);
        }
    }
}
