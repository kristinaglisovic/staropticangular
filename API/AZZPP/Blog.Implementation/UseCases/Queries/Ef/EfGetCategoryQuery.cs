using Blog.Application.Exeptions;
using Blog.Application.UseCases.DTO.Category;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetCategoryQuery : EfUseCase, IGetCategoryQuery
    {
        public EfGetCategoryQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 24;

        public string Name => "Get one category";

        public string Description => "Ef get one category by id";

        public CategoryDTO Execute(int id)
        {
            var cat = Context.Categories.Include(x => x.Posts).ThenInclude(x => x.Post).FirstOrDefault(x => x.Id == id);

            if (cat == null)
            {
                throw new EntityNotFoundException(nameof(Category), id);
            }

            return new CategoryDTO
            {
                Name = cat.Name,
                CreatedAt = cat.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                UpdatedAt = cat.UpdatedAt.HasValue ? cat.UpdatedAt.Value.ToString("yyyy-MM-dd hh:mm:ss") : "",
                IsActive = cat.IsActive,
                Id = cat.Id,
                PostsCount = cat.Posts.Count(),
                Description = cat.Desciption,
                ActivePostsCount=cat.Posts.Where(x=>x.Post.IsActive).Count()
            };
        }
    }
}
