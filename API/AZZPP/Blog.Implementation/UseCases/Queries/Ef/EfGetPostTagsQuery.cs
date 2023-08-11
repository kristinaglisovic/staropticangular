using Blog.Application.UseCases.DTO.PostTag;
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
    public class EfGetPostTagsQuery : EfUseCase, IGetPostTagsQuery
    {
        public EfGetPostTagsQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 42;

        public string Name => "Get post tags";

        public string Description => "Ef Get all post tags";

        public PagedResponse<PostTagDTO> Execute(PostTagsPagedSearch search)
        {


            var query = Context.PostTags.Include(x => x.Post).Include(x => x.Tag).AsQueryable();

            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Tag.Name.Contains(kw) || x.Post.Title.Contains(kw));
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

            var response = new PagedResponse<PostTagDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new PostTagDTO
            {
                PostId = x.PostId,
                PostName = x.Post.Title,
                TagId = x.TagId,
                TagName = x.Tag.Name
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;


        }
    
    }
}
