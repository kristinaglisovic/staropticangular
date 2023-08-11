using Blog.Application.UseCases.DTO.PostCategory;
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
    public class EfGetPostCategoryQuery : EfUseCase, IGetPostCategoryQuery
    {
        public EfGetPostCategoryQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 44;

        public string Name => "Get post category";

        public string Description => "Ef Get all posts with categories";

        public PagedResponse<PostCategoryDTO> Execute(PostCategoryPagedSearch search)
        {
            var query = Context.PostCategories.Include(x => x.Post).Include(x => x.Category).AsQueryable();

            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Category.Name.Contains(kw) || x.Post.Title.Contains(kw));
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

            var response = new PagedResponse<PostCategoryDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new PostCategoryDTO
            {
                PostId = x.PostId,
                PostName = x.Post.Title,
                CategoryId = x.CategoryId,
                CategoryName= x.Category.Name
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;

        }
    }
}
