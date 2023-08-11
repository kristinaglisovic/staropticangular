using Blog.Application.UseCases.DTO.PostImage;
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
    public class EfGetPostImagesQuery : EfUseCase, IGetPostImagesQuery
    {
        public EfGetPostImagesQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 43;

        public string Name => "Get all posts images";

        public string Description => "EF Get all posts and images";

        public PagedResponse<PostImagesDTO> Execute(PostImagesPagedSearch search)
        {
            var query = Context.PostImages.Include(x => x.Post).Include(x => x.Image).AsQueryable();

            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Image.Path.Contains(kw) || x.Post.Title.Contains(kw));
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

            var response = new PagedResponse<PostImagesDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new PostImagesDTO
            {
                PostId = x.PostId,
                PostName = x.Post.Title,
                ImageId = x.ImageId,
                ImagePath = x.Image.Path
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;
        }
    }
}
