using Blog.Application.UseCases.DTO.Image;
using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetImagesQuery : EfUseCase, IGetImagesQuery
    {
        public EfGetImagesQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => "Get all images";

        public string Description => "Get all with users and posts";

        public PagedResponse<ImageDTO> Execute(ImagePagedSearch search)
        {
            var query = Context.Images.Where(x => x.IsActive).Include(x => x.User).Include(x => x.Posts).ThenInclude(x => x.Post).AsQueryable();
                             

            var kw = search.Keyword;

            if (!string.IsNullOrEmpty(kw))
            {
                query=query.Where(x=>x.Path.Contains(kw) || x.User.FirstName.Contains(kw) 
                                  || x.User.LastName.Contains(kw) || x.User.Username.Contains(kw)
                                  || x.User.Email.Contains(kw) || x.Posts.Any(x=>x.Post.Title.Contains(kw)));   

            }

            if (search.PerPage == -1)
            {
                search.PerPage = query.Count();
            }

            if (search.PostsOnly.HasValue)
            {
                if (search.PostsOnly.Value)
                {
                    query = query.Where(p => p.User==null);

                }
                else
                {
                    query = query.Where(p => p.User != null);
                }
            }

            if (search.IsActive.HasValue)
            {
                if (search.IsActive.Value)
                {
                    query = query.Where(p => p.IsActive);
                }
                else
                {
                    query = query.Where(p => !p.IsActive);
                }

            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 5;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<ImageDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new ImageDTO
            {
                Id = x.Id,
                Path=x.Path,
                CreatedAt=x.CreatedAt,
                User = x.User != null ? $"{x.User.FirstName} {x.User.LastName} - {x.User.Username}" : "",
                Posts =x.Posts.Select(x=>new PostDto
                {
                    Id=x.PostId,
                    Title=x.Post.Title
                })
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;

        }
    }
}
