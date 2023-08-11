using Blog.Domain;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.DataAccess
{
    public class BlogDbContext : DbContext
    {

        public BlogDbContext(DbContextOptions options = null) : base(options)
        {

        }

        public IApplicationUser User { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<PostCategory>().HasKey(x => new { x.PostId, x.CategoryId });
            modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });
            modelBuilder.Entity<PostImage>().HasKey(x => new { x.PostId, x.ImageId });
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId });
            modelBuilder.Entity<Like>().Property(x=>x.IsActive).HasDefaultValue(true);
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        //{
        //1
        //    optionsbuilder.UseSqlServer(@"data source=kimix\sqlexpress;initial catalog=blog;integrated security=true");
        //2
        //    optionsbuilder.UseSqlServer(@"Data Source=KIMIX\\SQLEXPRESS;Initial Catalog=Blog;Integrated Security=True");

        //}



        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            break;
                       
                    }
                }   
            }

            return base.SaveChanges();
        }

        //DBSETOVI

        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories  { get; set; }
        public DbSet<PostImage> PostImages  { get; set; }
        public DbSet<Tag> Tags  { get; set; }
        public DbSet<PostTag> PostTags  { get; set; }
        public DbSet<User> Users  { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<TagTranslation> TagTranslations { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<PostTranslation> PostTranslations { get; set; }

        

    }
}
