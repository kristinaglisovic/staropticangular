using Blog.Application.Exeptions;
using Blog.Application.UseCases.DTO.Tag;
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
    public class EfGetTagQuery : EfUseCase, IGetTagQuery
    {
        public EfGetTagQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 19;

        public string Name => "Get one tag";

        public string Description => "Ef get one tag by id";

        public TagDTO Execute(int id)
        {
            var tag = Context.Tags.Include(x => x.Posts).ThenInclude(x => x.Post).FirstOrDefault(x => x.Id == id);

            if (tag == null)
            {
                throw new EntityNotFoundException(nameof(Tag), id);
            }

            return new TagDTO
            {
                Name = tag.Name,
                CreatedAt = tag.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                UpdatedAt = tag.UpdatedAt.HasValue ? tag.UpdatedAt.Value.ToString("yyyy-MM-dd hh:mm:ss") : "",
                IsActive = tag.IsActive,
                Id = tag.Id,
                PostsCount = tag.Posts.Count(),
                ActivePostsCount = tag.Posts.Where(x => x.Post.IsActive).Count()
            };
        }
    }
}

