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
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> 
        where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired()
                   .HasDefaultValueSql("GETDATE()");
            
           
            
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            ConfigureTableSpecifics(builder);
        }

        protected abstract void ConfigureTableSpecifics(EntityTypeBuilder<T> builder);
    }
}
