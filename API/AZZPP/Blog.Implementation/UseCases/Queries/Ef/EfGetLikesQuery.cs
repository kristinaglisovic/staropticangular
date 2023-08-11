using Blog.Application.UseCases.DTO.Like;
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
    public class EfGetLikesQuery : EfUseCase, IGetLikesQuery
    {
        public EfGetLikesQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 30;

        public string Name => "Get All likes";

        public string Description => "Get all likes info EF";

        public PagedResponse<LikeDTO> Execute(LikePagedSearch search)
        {
            var query = Context.Likes.Include(x => x.Post).Include(x => x.User).AsQueryable();


            var kw = search.Keyword;

            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.User.Username.Contains(kw) || x.Post.Title.Contains(kw) || x.User.Likes.Count.ToString().Contains(kw));

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


            var response = new PagedResponse<LikeDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new LikeDTO
            {
                Id = x.Id,
                isActive = x.IsActive,
                UserId = x.UserId,
                Username = x.User.Username,
                TotalLikes = x.User.Likes.Count,
                Post =
                       new PostDTO
                       {
                           Title = x.Post.Title,
                           Id = x.Post.Id,
                           CreatedAt = x.Post.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                       }
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;
        }
    }
}
