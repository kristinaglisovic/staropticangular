using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class CategoryTranslationConfiguration : EntityConfiguration<CategoryTranslation>
    {
        protected override void ConfigureTableSpecifics(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.Property(x => x.NameEng).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DesciptionEng).IsRequired();

            builder.HasIndex(x => x.NameEng).IsUnique();
        }
    }
}
