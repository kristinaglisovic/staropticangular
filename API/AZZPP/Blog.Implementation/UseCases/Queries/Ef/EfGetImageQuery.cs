using Blog.Application.Exeptions;
using Blog.Application.UseCases.DTO.Image;
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
    public class EfGetImageQuery : EfUseCase, IGetImageQuery
    {
        public EfGetImageQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Get image";

        public string Description => "Ef Get image by using an ID";

        public ImageDTO Execute(int id)
        {
            var image = Context.Images.Where(x => x.IsActive).Include(x => x.User).Include(x => x.Posts).ThenInclude(x => x.Post).FirstOrDefault(x=>x.Id==id);

            if (image == null)
            {
                throw new EntityNotFoundException(nameof(Image), id);
            }

            return new ImageDTO
            {
                Id = image.Id,
                Path = image.Path,
                CreatedAt = image.CreatedAt,
                User = image.User != null ? $"{image.User.FirstName} - {image.User.LastName} ({image.User.Username})" : "",
                Posts = image.Posts.Select(x => new PostDto
                {
                    Id = x.PostId,
                    Title = x.Post.Title
                }).ToList()
            };
        }
    }
}
