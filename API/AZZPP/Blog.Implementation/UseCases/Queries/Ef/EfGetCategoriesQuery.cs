using Blog.Application.UseCases.DTO.Category;
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
    public class EfGetCategoriesQuery : EfUseCase, IGetCategoriesQuery
    {
        public EfGetCategoriesQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id =>22;

        public string Name => "Get categories";

        public string Description => "Ef Get all categories";

        public PagedResponse<CategoryDTO> Execute(CategoriesPagedSearch search)
        {
            var query = Context.Categories.Include(x => x.Posts).ThenInclude(x => x.Post).Include(x=>x.CategoryTranslation).AsQueryable();


            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Name.Contains(kw) || x.Desciption.Contains(kw));

           }
            if (search.PerPage == -1)
            {
                search.PerPage = query.Count();
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

            if (search.PerPage == null || search.PerPage == 0)
            {
                search.PerPage = 4;
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



            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<CategoryDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new CategoryDTO
            {
                Name = search.Eng.HasValue && search.Eng.Value ? x.CategoryTranslation.NameEng : x.Name,
                NameSr = x.Name,
                NameUs = x.CategoryTranslation.NameEng,
                DescriptionSr=x.Desciption,
                DescriptionUs=x.CategoryTranslation.DesciptionEng,
                CreatedAt = x.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                UpdatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value.ToString("yyyy-MM-dd hh:mm:ss") : "",
                IsActive = x.IsActive,
                Id = x.Id,
                PostsCount = x.Posts.Count(),
                Description= search.Eng.HasValue && search.Eng.Value ? x.CategoryTranslation.DesciptionEng: x.Desciption,
                ActivePostsCount = x.Posts.Where(x => x.Post.IsActive).Count()

            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;
        }
    }
}
