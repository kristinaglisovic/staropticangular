using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.DTO.Tag;
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
    public class EfGetTagsQuery : EfUseCase, IGetTagsQuery
    {
        public EfGetTagsQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 18;

        public string Name => "Get all tags";

        public string Description => "Get all tags with pagination";

        public PagedResponse<TagDTO> Execute(TagPagedSearch search)
        {
            var query = Context.Tags.Include(x => x.Posts).ThenInclude(x => x.Post).Include(x=>x.TagTranslation).AsQueryable();


            var kw = search.Keyword;



            if (search.PerPage == -1)
            {
                search.PerPage = query.Count();
                
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



            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Name.Contains(kw));

            }



            if (search.HasPosts.HasValue)
            {
                if (search.HasPosts.Value)
                {
                    query = query.Where(p => p.Posts.Any());

                }
                else
                {
                    query = query.Where(p => !p.Posts.Any());
                }
            }


            if (search.IsPostActive.HasValue)
            {
                if (search.IsPostActive.Value)
                {
                    query = query.Where(p => p.Posts.Any(y=>y.Post.IsActive));

                }
                else
                {
                    query = query.Where(p => !p.Posts.Any(y => y.Post.IsActive));
                }
            }

            if (search.PerPage == null || search.PerPage == 0)
            {
                search.PerPage = 4;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<TagDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new TagDTO
            {
               
                Name = search.Eng.HasValue && search.Eng.Value ? x.TagTranslation.NameEng :x.Name,
                NameSr=x.Name,
                NameUs = x.TagTranslation.NameEng,
                CreatedAt = x.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                UpdatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value.ToString("yyyy-MM-dd hh:mm:ss") : "",
                IsActive=x.IsActive,
                Id=x.Id,
                PostsCount=x.Posts.Count(),
                ActivePostsCount=x.Posts.Where(x=>x.Post.IsActive).Count()
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;
        }
    }
}
