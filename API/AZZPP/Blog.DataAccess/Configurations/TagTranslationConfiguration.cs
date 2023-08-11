using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class TagTranslationConfiguration: EntityConfiguration<TagTranslation>
    {
     
        protected override void ConfigureTableSpecifics(EntityTypeBuilder<TagTranslation> builder)
        {
            builder.Property(x => x.NameEng).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.NameEng).IsUnique();

        }
    }
}
