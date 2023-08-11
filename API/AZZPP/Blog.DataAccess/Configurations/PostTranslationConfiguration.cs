using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class PostTranslationConfiguration : EntityConfiguration<PostTranslation>
    {
        protected override void ConfigureTableSpecifics(EntityTypeBuilder<PostTranslation> builder)
        {
            builder.Property(x => x.TitleEng).IsRequired().HasMaxLength(80);
            builder.Property(x => x.DescriptionEng).IsRequired();

            builder.HasIndex(x => x.TitleEng).IsUnique();
            builder.HasIndex(x => x.DescriptionEng);
            builder.HasIndex(x => x.Heading1Eng);
            builder.HasIndex(x => x.Heading2Eng);
            builder.HasIndex(x => x.Text1Eng);
            builder.HasIndex(x => x.Text2Eng);
        }
    }
}
